"use client";

import React, { useState } from "react";
import OutsideClickHandler from "react-outside-click-handler";
import Link from "next/link";
import Avatar from "./Avatar";
import Card from "./Card";
import PostItemCommentCount from "./PostItem";

export default function PostCard({ page, post, isOwnPost, onReload }) {
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [commentText, setCommentText] = useState("");
  const [commentCount, setCommentCount] = useState(post?.commentCount || 0);

  if (!post) return null;

  const getAuthHeaders = () => {
    const token = localStorage.getItem("token");
    return {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    };
  };

  const handleLike = async () => {
    try {
      const res = await fetch(`https://localhost:7182/api/Posts/${post.id}/like`, {
        method: "POST",
        headers: getAuthHeaders(),
      });
      if (!res.ok) throw new Error(await res.text());
      onReload?.();
    } catch (err) {
      console.error("Like thất bại:", err.message);
    }
  };

  const handleSaveOrUnsave = async () => {
    const isSavedPage = page === "SavePost";
    const method = isSavedPage ? "DELETE" : "POST";

    try {
      const res = await fetch(`https://localhost:7182/api/SavedPosts/${post.id}`, {
        method,
        headers: getAuthHeaders(),
      });

      if (!res.ok) throw new Error(await res.text());

      alert(isSavedPage ? "🗑️ Đã xoá khỏi mục đã lưu" : "✅ Đã lưu bài viết");
      if (isSavedPage) onReload?.();
    } catch (err) {
      console.error("Lưu/Xoá bài viết thất bại:", err.message);
    }
  };

const handleSubmitComment = async () => {
  if (!commentText.trim()) return;

  const token = localStorage.getItem("token");
  if (!token) {
    alert("Bạn cần đăng nhập để bình luận.");
    return;
  }

  try {
    const res = await fetch(`https://localhost:7182/api/Comments/${post.id}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ content: commentText.trim() }), 
    });

    const result = await res.text();
    console.log("Phản hồi:", result);

    if (!res.ok) throw new Error(result);

    setCommentText("");
    setCommentCount((prev) => prev + 1);
  } catch (err) {
    console.error("Lỗi gửi bình luận:", err.message);
  }
};

  const handleDeletePost = async () => {
    try {
      const res = await fetch(`https://localhost:7182/api/Posts/${post.id}`, {
        method: "DELETE",
        headers: getAuthHeaders(),
      });
      if (res.ok) {
        alert("🗑️ Đã xoá bài viết");
        onReload?.();
      }
    } catch (err) {
      console.error("Lỗi xoá bài viết:", err.message);
    }
  };

  return (
    <Card>
      {/* Header */}
      <div className="flex gap-3 items-center">
        <Avatar />
        <div className="grow">
          <span className="font-semibold">
            <Link href={`/profile?userId=${post.userId}`}>
              <p>{post.userName}</p>
            </Link>
          </span>
          <p className="text-gray-500 text-sm">{new Date(post.createdAt).toLocaleString()}</p>
        </div>

        {/* Dropdown */}
        <div className="relative">
          <button
            onClick={() => setDropdownOpen((prev) => !prev)}
            className="text-gray-400 p-2 hover:bg-gray-100 rounded"
            aria-label="Open options"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M6.75 12a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0ZM12.75 12a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0ZM18.75 12a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z"
              />
            </svg>
          </button>

          {dropdownOpen && (
            <OutsideClickHandler onOutsideClick={() => setDropdownOpen(false)}>
              <div className="absolute right-0 top-full mt-1 w-40 bg-white border border-gray-200 rounded-md p-1 shadow-md z-50">
                {isOwnPost ? (
                  <button
                    onClick={handleDeletePost}
                    className="w-full text-left p-2 text-red-600 hover:bg-gray-100 rounded"
                  >
                    🗑️ Xoá bài viết
                  </button>
                ) : (
                  <button
                    onClick={handleSaveOrUnsave}
                    className="w-full text-left p-2 hover:bg-gray-100 rounded"
                  >
                    {page === "SavePost" ? "❌ Bỏ lưu bài viết" : "📌 Lưu bài viết"}
                  </button>
                )}
              </div>
            </OutsideClickHandler>
          )}
        </div>
      </div>

      {/* Nội dung bài viết */}
      <div className="my-3 text-sm whitespace-pre-wrap">{post.content}</div>

      {/* Actions */}
      <div className="flex gap-7 mt-3">
        <button onClick={handleLike} className="flex gap-2 items-center hover:text-red-500 transition-colors">
          ❤️ {post.likesCount}
        </button>
        <PostItemCommentCount post={post} />
      </div>

      {/* Comment box */}
      <div className="flex mt-4 gap-3">
        <Avatar />
        <div className="grow relative border rounded-full">
          <textarea
            className="w-full p-2 px-4 h-12 overflow-hidden resize-none rounded-xl border-none"
            placeholder="Viết bình luận..."
            value={commentText}
            onChange={(e) => setCommentText(e.target.value)}
          />
          <button
            onClick={handleSubmitComment}
            className="absolute top-3 right-3 text-lg hover:text-blue-600"
            aria-label="Gửi bình luận"
          >
            <svg className="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m9 5 7 7-7 7"/>
</svg>

          </button>
        </div>
      </div>
    </Card>
  );
}

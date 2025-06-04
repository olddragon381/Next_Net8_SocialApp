"use client";

import React, { useEffect, useState } from "react";
import Card from "@/components/Card"; // hoặc đường dẫn tới Card
import Avatar from "@/components/Avatar";
import Link from "next/link";
import Layout from "@/components/Layout";
import PostCard from "@/components/PostCard";
import PostItemCommentCount from "@/components/PostItem";
import PostList from "@/components/PostList";

export default function SavePage() {
  const [savedPosts, setSavedPosts] = useState(null);
  const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;

  useEffect(() => {
    const fetchSavedPosts = async () => {
      try {
        const res = await fetch("https://localhost:7182/api/SavedPosts", {
          method: "GET",
          credentials: "include",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!res.ok) {
          throw new Error("Không thể lấy danh sách bài viết đã lưu");
        }

        const data = await res.json();
        setSavedPosts(data);
      } catch (error) {
        console.error("Lỗi khi lấy bài viết đã lưu:", error);
      }
    };

    if (token) {
      fetchSavedPosts();
    }
  }, [token]);

  if (!savedPosts) return <div>Loading...</div>;


  return (
      <Layout>
        <div>
      <h1 className="text-xl font-bold mb-4">Bài viết đã lưu</h1>
      {savedPosts.length === 0 && (
        <p>Không có bài viết nào đã lưu.</p>
      )}
       <PostList page={"SavePost"} posts={savedPosts} onReload={() => setReload(!reload)} />
    </div>
        
      </Layout>
  );

}

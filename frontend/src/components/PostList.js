"use client";
import React, { useState, useEffect } from "react";
import PostCard from "./PostCard";
import { jwtDecode } from "jwt-decode";

function getUserIdFromToken() {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    return decoded.userId || decoded.nameid; // hỗ trợ cả 2 claim
  } catch (err) {
    console.error("Token không hợp lệ:", err);
    return null;
  }
}

export default function PostList({ page, posts, onReload }) {
  const [currentUserId, setCurrentUserId] = useState(null);

  useEffect(() => {
    const uid = getUserIdFromToken();
    setCurrentUserId(uid);
  }, []);

  if (!posts) return <div>Loading...</div>;

  return (
    <div>
      {posts.map((post) => (
        <PostCard
          key={post.id}
          page={page}
          post={post}
          isOwnPost={post.userId === currentUserId} 
          onReload={onReload}
        />
      ))}
    </div>
  );
}

"use client";

import Layout from "@/components/Layout";
import PostCard from "@/components/PostCard";
import PostFormCard from "@/components/PostFromCard";
import PostList from "@/components/PostList";
import { useAuth } from "@/contexts/AuthContext";
import { useRouter } from "next/navigation";
import { useState, useEffect } from "react";

export default function Home() {
  const { user, loading } = useAuth();
  const router = useRouter();

  const [posts, setPosts] = useState([]);
  const [reload, setReload] = useState(false);

  useEffect(() => {
    if (!loading && !user) {
      router.push("/login");
    }
  }, [loading, user, router]);

  useEffect(() => {
    const fetchPosts = async () => {
      const token = localStorage.getItem("token");
      const res = await fetch("https://localhost:7182/api/Posts", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      const data = await res.json();
      setPosts(data);
    };

    if (user) {
      fetchPosts();
    }
  }, [reload, user]);

  if (loading || !user) return null;

  return (
    <Layout>
      <PostFormCard onPostSuccess={() => setReload(!reload)} />
      <PostList
        posts={posts}
        page="Home"
        onReload={() => setReload(!reload)}
        
      />
    </Layout>
  );
}

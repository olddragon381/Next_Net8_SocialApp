'use client';

import { useSearchParams } from 'next/navigation';
import { useEffect, useState } from 'react';
import Avartar from "@/components/Avatar";
import Card from "@/components/Card";
import Layout from "@/components/Layout";
import FriendCard from "@/components/FriendCard";
import PostList from '@/components/PostList';
import FriendButton from '@/components/FriendButton';

export default function Profile() {
  const searchParams = useSearchParams();

  const queryUserId = searchParams.get('userId');
  const [userId, setUserId] = useState(queryUserId);
  const [tab, setTab] = useState(searchParams.get('tab') || 'posts');
  const [user, setUser] = useState(null);
  const [posts, setPosts] = useState([]);
  const [reload, setReload] = useState(false); // ✅ Thêm reload

  const tabFromQuery = searchParams.get('tab') || 'posts';

  const tabClasses = 'flex gap-1 px-4 py-1 items-center';
  const activeTabClasses = 'flex gap-1 px-4 py-1 items-center border-b-4 border-blue-500 text-blue-400';
  const getTabClass = (name) => tab === name ? activeTabClasses : tabClasses;

  useEffect(() => {
    setTab(tabFromQuery);
  }, [tabFromQuery]);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        let res;
        if (!queryUserId) {
          const token = localStorage.getItem("token");
          res = await fetch(`https://localhost:7182/api/Auth/me`, {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          });
        } else {
          res = await fetch(`https://localhost:7182/api/Auth/${queryUserId}`);
        }

        const data = await res.json();
        setUser(data);

        if (!queryUserId && data.id) {
          setUserId(data.id);
        }
      } catch (err) {
        console.error("Lỗi khi lấy user:", err);
      }
    };

    fetchUser();
  }, [queryUserId]);

  useEffect(() => {
    const fetchPosts = async () => {
      if (!userId) return;
      try {
        const res = await fetch(`https://localhost:7182/api/Posts/user/${userId}`);
        const data = await res.json();
        setPosts(data);
      } catch (err) {
        console.error("Lỗi khi lấy bài viết:", err);
      }
    };

    fetchPosts();
  }, [userId, reload]); // ✅ thêm reload để cập nhật khi cần

  return (
    <Layout>
      <Card noPadding={true}>
        <div className="relative overflow-hidden rounded-md">
          <div className="h-40 overflow-hidden flex justify-center items-center">
            <img
              src="https://images.unsplash.com/photo-1734630630491-458df4f38213?q=80&w=1974&auto=format&fit=crop"
              className="w-full h-full object-cover rounded-lg"
              alt="cover"
            />
          </div>
          <div className="absolute top-12 left-4">
            <Avartar size={'big'} />
          </div>

          <div className="p-4 pb-1">
            <div className="ml-40 ">
              <div className='flex justify-between items-center'>
               <div>
                   <h1 className="text-3xl font-bold">
                {user ? user.userName || user.username || "Không rõ tên" : "Đang tải..."}
              </h1>
              {user?.email && (
                <p className="text-gray-500 text-sm">{user.email}</p>
              )}
               </div>
                 
              {user?.id && (
                <FriendButton targetUserId={user.id}></FriendButton>
              )}
              
                
              </div>
              <div className="mt-10 flex gap-0">
                <button onClick={() => setTab("posts")}>
                  <span className={getTabClass('posts')}>📝 Bài viết</span>
                </button>
                <button onClick={() => setTab("friends")}>
                  <span className={getTabClass('friends')}>👥 Bạn bè</span>
                </button>
              </div>
              
              
            </div>
          </div>
        </div>
      </Card>

      <div className="mt-4">
        {tab === "posts" && (
          <PostList
            posts={posts}
            page="Home"
            onReload={() => setReload(prev => !prev)}
          />
        )}
        {tab === "friends" && <FriendCard userId={userId} />}
      </div>
    </Layout>
  );
}

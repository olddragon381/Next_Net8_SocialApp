"use client";

import { useEffect, useState } from "react";
import Card from "./Card";
import FriendInfo from "./FriendInfo";

export default function FriendCard({ userId }) {
  const [friends, setFriends] = useState([]);
  const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;

  useEffect(() => {
    if (!userId || !token) return;

    const fetchFriends = async () => {
      const res = await fetch(`https://localhost:7182/api/Friend/${userId}/friends`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (res.ok) {
        const data = await res.json();
        console.log("Friend data:", data);

        setFriends(data);
      }
    };

    fetchFriends();
  }, [userId, token]);

  return (
    <Card>
      <h2 className="text-3xl mb-2 font-bold border-b border-b-gray-200">
        Bạn bè ({friends.length})
      </h2>

      <div className="grid gap-4 py-4 grid-cols-3">
  {friends.map((friend) => (
    <FriendInfo
      key={friend.id}
      name={friend.username}       // Dùng đúng trường username
      email={friend.email}
      
    />
  ))}
</div>

    </Card>
  );
}

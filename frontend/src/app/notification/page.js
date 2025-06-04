"use client";

import { useEffect, useState } from "react";
import { useAuth } from "@/contexts/AuthContext";
import Layout from "@/components/Layout";
import Card from "@/components/Card";
import Avartar from "@/components/Avatar";

export default function NotificationPage() {
  const { user } = useAuth();
  const [notifications, setNotifications] = useState([]);
  const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;

  const fetchNotifications = async () => {
    if (!user || !token) return;

    const res = await fetch(`https://localhost:7182/api/Notification/${user.id}`, {
      headers: { Authorization: `Bearer ${token}` },
    });

    const data = await res.json();
    setNotifications(data);
  };

  const markAsRead = async (id) => {
    await fetch(`https://localhost:7182/api/Notification/mark-as-read/${id}`, {
      method: "POST",
      headers: { Authorization: `Bearer ${token}` },
    });

    setNotifications((prev) =>
      prev.map((n) => (n.id === id ? { ...n, isRead: true } : n))
    );
  };

  const handleAccept = async (notification) => {
  await fetch(`https://localhost:7182/api/Friend/accept/${notification.requestId}`, {
    method: "POST",
    headers: { Authorization: `Bearer ${token}` },
  });

  await markAsRead(notification.id);
};

const handleReject = async (notification) => {
  await fetch(`https://localhost:7182/api/Friend/reject/${notification.requestId}`, {
    method: "POST",
    headers: { Authorization: `Bearer ${token}` },
  });

  await markAsRead(notification.id);
};

  useEffect(() => {
    fetchNotifications();
  }, [user]);
  return (
      <Layout>
        <div>
           
        <Card noPadding={true}>
          <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Thông báo</h1>
      {notifications.map((n) => (

  <div key={n.id} className={`flex gap-3 items-center border-b border-b-gray-100 p-4 border rounded shadow ${n.isRead ? "bg-gray-100" : "bg-white"}`}>
    <Avartar></Avartar>
    <h6>{n.title}</h6>
    <p>{n.content}</p>
    <small className="text-gray-500">{new Date(n.createdAt).toLocaleString()}</small>


    {n.type == 0 && !n.isRead && (
      <div className="mt-2 flex gap-2">
        <button
          onClick={() => handleAccept(n)}
          className="bg-green-500 text-white px-3 py-1 rounded"
        >
          Chấp nhận
        </button>
        <button
          onClick={() => handleReject(n)}
          className="bg-red-500 text-white px-3 py-1 rounded"
        >
          Từ chối
        </button>
      </div>
    )}

    {n.type !== 0 && !n.isRead && (
      <button onClick={() => markAsRead(n.id)} className="ml-4 text-sm text-blue-600 underline">
        Đánh dấu đã đọc
      </button>
    )}
  </div>
))}



      
    </div>

          

        </Card>
        </div>
        
      </Layout>
  );
}
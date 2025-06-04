"use client";

import { useEffect, useState } from "react";
import { useAuth } from "@/contexts/AuthContext";

export default function FriendButton({ targetUserId }) {
  const { user } = useAuth();
  const [status, setStatus] = useState("loading"); // loading, none, sent, received, friends
  const [requestId, setRequestId] = useState(null);
  const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;

  useEffect(() => {
    if (!user || !token || !targetUserId || user.id === targetUserId) return;

    const fetchStatus = async () => {
  try {
    const [received, sent, friends] = await Promise.all([
      fetch(`https://localhost:7182/api/Friend/received/${user.id}`, {
        headers: { Authorization: `Bearer ${token}` },
      }),
      fetch(`https://localhost:7182/api/Friend/sent/${user.id}`, {
        headers: { Authorization: `Bearer ${token}` },
      }),
      fetch(`https://localhost:7182/api/Friend/${user.id}/friends`, {
        headers: { Authorization: `Bearer ${token}` },
      }),
    ]);

    const receivedData = received.ok ? await received.json() : [];
    const sentData = sent.ok ? await sent.json() : [];
    const friendsData = friends.ok ? await friends.json() : [];

    const isFriend = friendsData.find(
      (f) => f.id === targetUserId || f._id === targetUserId
    );
    if (isFriend) {
      setStatus("friends");
      return;
    }

    const sentRequest = sentData.find((r) => r.receiverId === targetUserId);
    if (sentRequest) {
      console.log("Accepting requestId:", requestId);
      setStatus("sent");

      setRequestId(sentRequest.Id);
      return;
    }

    const receivedRequest = receivedData.find((r) => r.senderId === targetUserId);
    if (receivedRequest) {
      setStatus("received");
      setRequestId(receivedRequest.id);
      return;
    }

    setStatus("none");
  } catch (error) {
    console.error("Lỗi khi fetch trạng thái kết bạn:", error);
    setStatus("none");
  }
};

    fetchStatus();
  }, [targetUserId, user, token]);

  const sendRequest = async () => {
    await fetch("https://localhost:7182/api/Friend/request", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ senderId: user.id, receiverId: targetUserId }),
    });
    setStatus("sent");
  };

  const acceptRequest = async () => {
    await fetch(`https://localhost:7182/api/Friend/accept/${requestId}`, {
      method: "POST",
      headers: { Authorization: `Bearer ${token}` },
    });
    setStatus("friends");
  };

  const rejectRequest = async () => {
    await fetch(`https://localhost:7182/api/Friend/reject/${requestId}`, {
      method: "POST",
      headers: { Authorization: `Bearer ${token}` },
    });
    setStatus("none");
  };

  const unfriend = async () => {
    await fetch(`https://localhost:7182/api/Friend/unfriend`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ userId1: user.id, userId2: targetUserId }),
    });
    setStatus("none");
  };

  const renderButton = () => {
    switch (status) {
      case "loading":
        return  <button className="bg-gray-400 text-white px-4 py-2 rounded hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded inline-flex items-center">
  <span>Đang tải...</span>
</button>
        
    
      case "none":
        return  <button onClick={sendRequest}  className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded inline-flex items-center">
        <svg className="w-6 h-6 text-white  dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M16 12h4m-2 2v-4M4 18v-1a3 3 0 0 1 3-3h4a3 3 0 0 1 3 3v1a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1Zm8-10a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"/>
</svg>
  <span>Kết bạn</span>
</button>
      case "sent":
         return  <button  className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded inline-flex items-center">
        <svg className="w-6 h-6 text-white dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M16 12h4m-2 2v-4M4 18v-1a3 3 0 0 1 3-3h4a3 3 0 0 1 3 3v1a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1Zm8-10a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"/>
</svg>
  <span>Đã gửi kết bạn</span>
</button>
      case "received":
        return (
          <div className="flex gap-2">
            <button onClick={acceptRequest}  className="bg-green-600 text-white px-4 py-2 rounded hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded inline-flex items-center">
        <svg className="w-6 h-6 text-white dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M16 12h4m-2 2v-4M4 18v-1a3 3 0 0 1 3-3h4a3 3 0 0 1 3 3v1a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1Zm8-10a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"/>
</svg>
  <span>Chấp nhận</span>
</button>
            <button onClick={rejectRequest}  className="bg-red-600 text-white px-4 py-2 rounded hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded inline-flex items-center">
        <svg className="w-6 h-6 text-white  dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M16 12h4m-2 2v-4M4 18v-1a3 3 0 0 1 3-3h4a3 3 0 0 1 3 3v1a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1Zm8-10a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"/>
</svg>
  <span>Từ chối</span>
</button>

          </div>
        );
      case "friends":
        return <button onClick={unfriend}  className="bg-gray-600 text-white px-4 py-2 rounded hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded inline-flex items-center">
        <svg className="w-6 h-6 text-white  dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M16 12h4m-2 2v-4M4 18v-1a3 3 0 0 1 3-3h4a3 3 0 0 1 3 3v1a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1Zm8-10a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"/>
</svg>
  <span>Hủy kết bạn</span>
</button>
      default:
        return null;
    }
  };

  return <div>{renderButton()}</div>;
}

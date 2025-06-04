"use client";

import Card from "@/components/Card";
import Layout from "@/components/Layout";
import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/contexts/AuthContext";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { setUser, user, loading } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!loading && user) {
      router.push("/");
    }
  }, [user, loading, router]);

  const handleLogin = async (e) => {
    e.preventDefault();
    if (!email.includes("@")) {
  alert("Email không hợp lệ. Phải chứa ký tự @");
  return;
}
    try {
      // Gửi yêu cầu đăng nhập
      const loginRes = await fetch("https://localhost:7182/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          email: email,
          passwordHash: password,
        }),
      });

      if (!loginRes.ok) {
        throw new Error(await loginRes.text());
      }

      const loginData = await loginRes.json();
      const token = loginData.token;

      // Gọi /me để lấy thông tin người dùng (có gắn token)
      const meRes = await fetch("https://localhost:7182/api/Auth/me", {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!meRes.ok) {
        throw new Error(await meRes.text());
      }

      const currentUser = await meRes.json();

      // Lưu token nếu muốn (localStorage/sessionStorage)
      localStorage.setItem("token", token);

      // Cập nhật context
      setUser(currentUser);

      // Điều hướng
      router.push("/");
    } catch (err) {
      console.error("Login failed:", err);
      alert("Đăng nhập thất bại.");
    }
  };

  return (
    <Layout hideNavigation={true}>
      <div className="h-screen flex items-center justify-center bg-gray-100">
        <div className="max-w-sm w-full">
          <h1 className="text-4xl font-bold text-center text-gray-700 mb-6">Đăng nhập</h1>
          <Card noPadding={true}>
            <form onSubmit={handleLogin} className="bg-white p-6 rounded shadow-md">
              <input
                type="text"
                placeholder="Tên đăng nhập"
                className="w-full p-2 mb-4 border border-gray-300 rounded"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />

              <input
                type="password"
                placeholder="Mật khẩu"
                className="w-full p-2 mb-4 border border-gray-300 rounded"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />

              <button
                type="submit"
                className="w-full bg-blue-600 text-white p-2 rounded hover:bg-blue-700"
              >
                Đăng nhập
              </button>
            </form>

            <p className="mt-4 text-center text-sm text-gray-600">
              Chưa có tài khoản?{" "}
              <a href="/register" className="text-blue-500 hover:underline">
                Đăng ký ngay
              </a>
            </p>
          </Card>
        </div>
      </div>
    </Layout>
  );
}

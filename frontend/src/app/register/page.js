'use client'
import Card from '@/components/Card'
import Layout from '@/components/Layout'
import { useState } from 'react'

export default function RegisterPage() {
  const [Email, setEmail] = useState('')
  const [Username, setUserName] = useState('')
  const [Password, setPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')

  const handleRegister = async (e) => {
    e.preventDefault();
    
    try {
      var userName = Username;
     var email = Email;
      var passwordHash = Password;

      const res = await fetch("https://localhost:7182/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userName, passwordHash, email }),
      });
  
      if (!res.ok) throw new Error("Đăng ký thất bại");
  
      alert("Đăng ký thành công");
    } catch (err) {
      console.error("Register failed:", err);
      alert("Đăng ký thất bại");
    }
  };

  return (

    <Layout hideNavigation={true}>
        <div className="h-screen flex items-center">
        <div className="max-w-xs mx-auto grow -mt-24">
        <h2 className="text-6xl mb-4 text-gray-300 text-center">Đăng ký</h2>
        <Card noPadding={true}>
        <div className="flex items-center justify-center bg-gray-100">

<form onSubmit={handleRegister} className="bg-white p-8 rounded shadow-md w-80">
 

  <input
    type="Email"
    placeholder="Email"
    className="w-full p-2 mb-4 border border-gray-300 rounded"
    value={Email}
    onChange={(e) => setEmail(e.target.value)}
  />

<input
    type="Username"
    placeholder="Username"
    className="w-full p-2 mb-4 border border-gray-300 rounded"
    value={Username}
    onChange={(e) => setUserName(e.target.value)}
  />

  <input
    type="Password"
    placeholder="Mật khẩu"
    className="w-full p-2 mb-4 border border-gray-300 rounded"
    value={Password}
    onChange={(e) => setPassword(e.target.value)}
  />

  <input
    type="Password"
    placeholder="Xác nhận mật khẩu"
    className="w-full p-2 mb-4 border border-gray-300 rounded"
    value={confirmPassword}
    onChange={(e) => setConfirmPassword(e.target.value)}
  />

  <button
    type="submit"
    className="w-full bg-green-200 text-white p-2 rounded hover:bg-green-400"
  >
    Đăng ký
  </button>
</form>
</div>

        </Card>
        <p className="mt-4 text-center text-sm text-gray-600">
  Đã có tài khoản?{" "}
  <a href="/login" className="text-blue-500 hover:underline">
    Đăng nhập
  </a> 
  </p>
        </div>
        
        </div>

    </Layout>

   
  )
}
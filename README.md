# 🧠 Mạng Xã Hội
![image-1](https://github.com/user-attachments/assets/4ff4964e-7fae-44b4-92fb-efc1a3899d93)
![image](https://github.com/user-attachments/assets/d0ccaaec-aaf6-4c20-ba0a-533090744535)
![image-4](https://github.com/user-attachments/assets/3868389b-bd89-4439-87f7-8b6a36ad4e89)
![image-3](https://github.com/user-attachments/assets/8865efd9-d6a2-4306-b232-380f0adcf738)
![image-2](https://github.com/user-attachments/assets/ee1ba22e-45ab-44a9-a767-850ca55580b1)
![backend1](https://github.com/user-attachments/assets/9894c04e-a8b3-424a-83ca-5ac1805a7891)
![backend2](https://github.com/user-attachments/assets/d16bec51-096e-42c0-bc4b-b3a1eceb021a)



## 📌 Giới thiệu
Công nghệ: ASP.Net API 8, NextJS, Tailwind, JWT, SignalR

Đây là một nền tảng mạng xã hội nơi người dùng có thể:
- 📝 Đăng bài viết
- ❤️ Thả tim bài viết
- 💬 Bình luận bài viết
- 📌 Lưu hoặc bỏ lưu bài viết
- 🗑️ Xoá bài viết của chính mình


Hệ thống bao gồm:

- **Frontend:** Next.js (JavaScript)
- **Backend:** ASP.NET Core (.NET 8) theo kiến trúc **Clean Architecture**, SignalR
- **Cơ sở dữ liệu:** MongoDB
- **Xác thực:** JWT Token
---

## 🏗️ Kiến trúc hệ thống

### 📁 Backend - Clean Architecture
- `Application`: Chứa business logic và interface
- `Domain`: Khai báo các entity (Post, Comment, User…)
- `Infrastructure`: Triển khai repository dùng MongoDB
- `WebApi`: Controller, cấu hình API và JWT

### ⚛️ Frontend - Next.js
- Trang đăng bài, xem bài, lưu bài, thông báo
- Giao diện hiện đại, sử dụng Tailwind CSS
- Tích hợp fetch API backend với JWT từ localStorage

---

## 🚀 Hướng dẫn chạy

### 1. Backend (.NET 8)

```bash
cd Backend
dotnet restore
dotnet run


📌 Các tính năng đã hoàn thành
✅ Đăng ký, đăng nhập (JWT)

✅ Đăng bài viết

✅ Thích bài viết

✅ Bình luận bài viết

✅ Lưu / bỏ lưu bài viết

✅ Xoá bài viết (nếu là chủ bài)

✅ Hiển thị số lượng like, comment

✅ Trang thông báo kết bạn


---
  



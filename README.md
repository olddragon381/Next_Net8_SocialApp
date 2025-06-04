# 🧠 Mạng Xã Hội
![backend1](https://github.com/user-attachments/assets/c3e59856-b3f3-460c-b6f1-0667bbac1963)
![backend2](https://github.com/user-attachments/assets/83c21ad4-fb9e-495e-a4b1-39d16d4f27b2)
![image](https://github.com/user-attachments/assets/8cd45151-8424-43b4-9428-61b6790eebca)
![image-4](https://github.com/user-attachments/assets/8b176c32-9815-42a0-bdb8-2e3b96d919c9)
![image-3](https://github.com/user-attachments/assets/e33200f5-5fbe-4f8a-93ac-5a6246b7beda)
![image-2](https://github.com/user-attachments/assets/554f17ea-aa25-44a9-b60c-ff101ab853df)
![image-1](https://github.com/user-attachments/assets/6597d06b-a61e-4e34-861d-9cc16208604a)



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
  



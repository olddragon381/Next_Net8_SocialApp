// Đăng ký người dùng
export async function register(data) {
  const res = await fetch(`${process.env.NEXT_PUBLIC_API_BASE_URL}/api/Auth/register`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    credentials: 'include',
    body: JSON.stringify(data),
  });

  if (!res.ok) throw new Error(await res.text());
  return await res.text();
}

// Đăng nhập
export async function login({ UserName, PasswordHash }) {
  const res = await fetch(`https://localhost:7182/api/auth/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
  
    body: JSON.stringify({ UserName, PasswordHash }),
  });

  if (!res.ok) {
    throw new Error("Đăng nhập thất bại");
  }

  const data = await res.json();
  localStorage.setItem("token", data.accessToken); // lưu token
  return data;
}

// Lấy thông tin người dùng hiện tại (đã đăng nhập)
export async function getCurrentUser() {
  const res = await fetch(`${process.env.NEXT_PUBLIC_API_BASE_URL}/api/auth/me`, {
    method: 'GET',
    credentials: 'include',
  });

  if (!res.ok) throw new Error(await res.text());
  return await res.json();
}

export const logout = async () => {
  return fetch("https://localhost:7182/api/Auth/logout", {
    method: "POST",
    credentials: "include",
  });
}
export async function savePost(postId) {
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_BASE_URL}/posts/save/${postId}`, {
      method: 'POST',
      credentials: 'include', // gửi cookie JWT
    });
  
    if (!res.ok) {
      throw new Error('Failed to save post');
    }
  
    return await res.text(); // hoặc .json() nếu API trả về object
  }


  export const createPost = async (content) => {
    const token = localStorage.getItem("token"); // Lấy token đã lưu
    const userId = "";
    const res = await fetch("https://localhost:7182/api/Posts", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`,  // Gửi JWT
      },
      body: JSON.stringify({ content , userId}),  // Không cần gửi userId nữa
    });
  
    if (!res.ok) {
      const error = await res.text();
      throw new Error("Lỗi đăng bài: " + error);
    }
  
    return await res.json(); 
  };
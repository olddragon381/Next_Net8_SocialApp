'use client';

import { useEffect, useState } from 'react';
import { useParams } from 'next/navigation';
import Card from '@/components/Card';
import Layout from '@/components/Layout';

export default function PostCommentPage() {
  const { id } = useParams(); // ✅ lấy postId từ URL
  const [comments, setComments] = useState([]);

  useEffect(() => {
    if (!id) return;

    fetch(`https://localhost:7182/api/Comments/${id}`)
      .then((res) => res.json())
      .then((data) => setComments(data))
      .catch((err) => console.error('Lỗi khi gọi API:', err));
  }, [id]);

  return (
    <Layout>
        
          <Card>
       
        <ul>
          {comments.map((c) => (
            <li key={c.id}>
              <strong>{c.username}</strong>: {c.content}
              <br />
              <small>{new Date(c.commentedAt).toLocaleString()}</small>
            </li>
          ))}
        </ul>
      

    </Card>
        </Layout>
    
    
  );
}

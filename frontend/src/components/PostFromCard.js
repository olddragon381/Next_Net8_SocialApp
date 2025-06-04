import { createPost } from "@/services/postService";
import Avartar from "./Avatar";
import Card from "./Card";
import { useState } from "react";


export default function PostFormCard({ onPostSuccess }) {
  const [content, setContent] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!content.trim()) return;

    setLoading(true);
    try {

      
      await createPost(content);
      setContent("");
      if (onPostSuccess) onPostSuccess(); // reload feed
    } catch (err) {
      alert(err.message);
    } finally {
      setLoading(false);
    }
  };



    return (
     <Card>
      <form onSubmit={handleSubmit}>
        <div className="flex gap-2">
                    <Avartar/>
                
                  <textarea value={content}
        onChange={(e) => setContent(e.target.value)}
        className= "grow py-3 h-14" placeholder="Bạn nghĩ gì?"></textarea>  
                </div>



            

            <div className ="grow text-right">
                <button type="submit"
        disabled={loading} className="bg-blue-500 text-white px-6 py-1 rounded-md">{loading ? "Đang chia sẽ..." : "Chia sẽ"}
        </button>

            </div>
         
   

        
      </form>
        

        
     </Card>
    )
}
'use client';

import { useRouter, usePathname } from 'next/navigation';
import { useAuth } from "@/contexts/AuthContext";
import Card from "./Card";
import Link from "next/link";

export default function NavigationCard(){
        const router = useRouter();
        const pathname = usePathname();
      
        console.log('Router:', router);
        console.log('Pathname:', pathname);
        const { user, logout } = useAuth();


        const handleLogout = async () => {
          try {
            const res = await fetch("https://localhost:7182/api/Auth/logout", {
              method: "POST",
              credentials: "include", 
            });
            const text = await res.text(); // xem chi tiết lỗi
            console.log("Status:", res.status);
            console.log("Response:", text);
            if (!res.ok) throw new Error("Logout failed");
        
            logout(); // xóa user trong context
            router.push("/login");
          } catch (err) {
            console.error("Logout failed:", err);
            alert("Đăng xuất thất bại.");
          }
        };



        const activeElenment= 'flex gap-4 py-2 bg-blue-500  text-white -mx-10 px-10 rounded-md shadow-md shadow-gray-300';
        const nonActiveElement = 'flex gap-4 py-2 hover:bg-blue-200 -mx-6 px-6 rounded-md transition-all hover:scale-110 hover:shadow-md shadow-gray-300';



    return (
        <Card noPadding={true}>
          {user ? (
        <>
        <div className="px-4 py-2 flex justify-between md:block shadow-md shadow-gray-500 md:shadow-none">
          <h2 className="text-gray-400 mb-3 hidden md:block">{user.UserName}</h2>
          <h2 className="text-gray-400 mb-3 hidden md:block">Navigation</h2>
          <Link href="/" className={pathname === '/' ? activeElenment : nonActiveElement}>
            <svg className="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m4 12 8-8 8 8M6 10.5V19a1 1 0 0 0 1 1h3v-3a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v3h3a1 1 0 0 0 1-1v-8.5"/>
</svg>

            <span className="hidden md:block">Home</span>
          </Link>
          <Link href="http://localhost:3000/profile" className={pathname === '/profile' ? activeElenment : nonActiveElement}>
            <svg className="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeWidth="2" d="M16 19h4a1 1 0 0 0 1-1v-1a3 3 0 0 0-3-3h-2m-2.236-4a3 3 0 1 0 0-4M3 18v-1a3 3 0 0 1 3-3h4a3 3 0 0 1 3 3v1a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1Zm8-10a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"/>
</svg>

            <span className="hidden md:block">Profile</span>
          </Link>
          <Link href="/notification" className={pathname === '/notification' ? activeElenment : nonActiveElement}>
            <svg className="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m10.827 5.465-.435-2.324m.435 2.324a5.338 5.338 0 0 1 6.033 4.333l.331 1.769c.44 2.345 2.383 2.588 2.6 3.761.11.586.22 1.171-.31 1.271l-12.7 2.377c-.529.099-.639-.488-.749-1.074C5.813 16.73 7.538 15.8 7.1 13.455c-.219-1.169.218 1.162-.33-1.769a5.338 5.338 0 0 1 4.058-6.221Zm-7.046 4.41c.143-1.877.822-3.461 2.086-4.856m2.646 13.633a3.472 3.472 0 0 0 6.728-.777l.09-.5-6.818 1.277Z"/>
</svg>

            <span className="hidden md:block">Notification</span>
          </Link>
          <Link href="/save" className={pathname === '/saved' ? activeElenment : nonActiveElement}>
           <svg className="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m17 21-5-4-5 4V3.889a.92.92 0 0 1 .244-.629.808.808 0 0 1 .59-.26h8.333a.81.81 0 0 1 .589.26.92.92 0 0 1 .244.63V21Z"/>
</svg>

            <span className="hidden md:block">Saved posts</span>
          </Link>
          
          <div  className="w-full ">
            <span className={nonActiveElement}>
              <svg className="w-6 h-6" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M20 12H8m12 0-4 4m4-4-4-4M9 4H7a3 3 0 0 0-3 3v10a3 3 0 0 0 3 3h2"/>
</svg>

              <button className="hidden md:block" onClick={handleLogout}>Logout</button>
             
            </span>
          </div>
        </div>

        </>
      ) : (
        <p>Not logged in</p>
      )}

          
        
      </Card>
    );
    
}
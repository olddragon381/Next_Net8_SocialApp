import Avartar from "./Avatar";
import Card from "./Card";

export default function FriendInfo({name, email}){
    
    return(
    <div className="flex gap-2">
        <Avartar/>
        <div>
            <h3 className="font-bold text-xl">{name}</h3>
            <p >{email}</p>
           
        </div>    
    </div>
    )
}
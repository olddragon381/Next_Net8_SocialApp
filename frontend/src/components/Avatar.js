export default function Avartar({size}){
    let Size = 'w-12 h-12 ';
    if (size === 'big'){
        Size='w-36 h-36 '
    }
    return(
        <div>
               <div className={`${Size} rounded-full overflow-hidden`}>
                <img src = "https://plus.unsplash.com/premium_photo-1673266633993-013acd448898?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"></img>
                </div>
            </div>
    )
}
import React from 'react'; 
import { User } from '../Domain/User';
import GetAvatar from './GetAvatar';

interface UserAtRoomListProps {
  Clients: User[];

  HttpEndPoint : string;
  WsEndPoint : string;
//   self : string
}

const UserAtRoomList: React.FC<UserAtRoomListProps> = ({ Clients, WsEndPoint,HttpEndPoint, /*self*/ }) => {
  return (
    <div style={{gap:"30px", width:"100%", }}>
      
        {Clients.map((client) => (
            
            <div key={client.UserID} style={{display:"flex",widows:"100%", marginBottom:"10px",padding:"10px",gap:"10px", fontSize:"1em", backgroundColor:"rgb(37,37,37)",borderRadius:"10px"}}>
                <GetAvatar Id={client.UserID} HttpEndPoint={HttpEndPoint} WsEndPoint={WsEndPoint} username = {client.UserName}/>
                <div style={{display:"flex", justifyContent:"center", alignItems:"center"}}>{client.UserName}</div>
            </div>
      
        ))}

    </div>
  );
};

export default UserAtRoomList;

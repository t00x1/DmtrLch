import React, { useEffect, useState } from 'react';

import styles from './CasinoWheel.module.css';
import { responseStatus, apiResponse, userDTO } from '../Domain/types';
import { BetDataWheel } from '../Domain/BetDataWheel';
import { EventResult } from '../Domain/EventResult';
import { QueryData } from '../Domain/QueryData';
import { User,DataStructure } from '../Domain/User';
import { Console } from 'console';
import { parse } from 'path';
import CasinoWheelElem from './CasinoWheelElem';

import GetAvatar from '../Modules/GetAvatar';
import UserAtRoomList from '../Modules/UserAtRoomList';
import Balance from '../Domain/Balance/Balance';
import { Navigate, useNavigate } from 'react-router-dom';


interface CasinoWheelProps {
  webSocket: WebSocket | null; // URL до API
  message: string[];
  HttpEndPoint : string;
  WsEndPoint : string;
}

const CasinoWheel: React.FC<CasinoWheelProps> = ({ webSocket, message, HttpEndPoint, WsEndPoint }) => {
  const [bet, setBet] = useState<number>();

  const navigate = useNavigate();

  const [mustSpin, setMustSpin] = useState(false);
  const [prizeNumber, setPrizeNumber] = useState(0);
  const [BetsOfUsers, setBetsOfUsers] = useState<BetDataWheel[]>([]);
  

  const handleSpinClick = ( number:string) => {
    const newPrizeNumber = parseInt(number, 10);
    setPrizeNumber(newPrizeNumber);
    setMustSpin(true);

    // Отправка данных через WebSocket
    
  };

  const [users, setUsers] = useState<User[]>([])
  const [balanceKey, setBalanceKey] = useState<number>(0);
  useEffect(()=>{
    if(!mustSpin){
      setBalanceKey(prevKey => prevKey + 1);
      setBetsOfUsers([])
    }
  },[mustSpin])
  const [WonBetelem, setWonBetelem] = useState<string>()
  useEffect(() =>{console.log(users)},[users])
  useEffect(() =>{
    if (message != undefined) {
      const newMsg: string = message[message.length - 1];
      if(newMsg!= undefined ) 
      console.log(message);
    
      
      try{

        const parsedMessage: apiResponse<EventResult<object>> = JSON.parse(newMsg);
        console.log(parsedMessage)
        if(parsedMessage.IsSuccess)
        {

            switch(parsedMessage.Data?.Event){
              case "RoundEnded":
                if (typeof parsedMessage.Data?.Data === "string") {
                  handleSpinClick(parsedMessage.Data.Data);
                  setWonBetelem(parsedMessage.Data?.Data)
                } else {
                  console.error("Data is not a string:", parsedMessage.Data?.Data);
                }
                break
              case "join":
    
                console.log("ПОЛЬЗОВАТЕЛИ")
                const dataStructure: DataStructure = parsedMessage.Data.Data as DataStructure;
              
              
                console.log(dataStructure)
                setUsers(dataStructure.IDOfClients);
              
              break; 
              case  "BetTaken":
              console.log(parsedMessage)
              const BetDataWheel : BetDataWheel = parsedMessage.Data.Data as BetDataWheel
              setBetsOfUsers((e) => [...e, BetDataWheel]);
              break;
              case "ClientEntered":
                const User: User = parsedMessage.Data.Data as User
                setUsers(prevUsers => [...prevUsers, User ])
                break;
                case "ClientExited":
              
                if (typeof parsedMessage.Data?.Data === "string") {
                  const IdOfUser: string = parsedMessage.Data?.Data
                  setUsers(prevUsers => prevUsers.filter(user => user.UserID !== IdOfUser));
                } else {
                  console.error("Data is not a string:", parsedMessage.Data?.Data);
                }
              break;
    
                default:
                  console.log(parsedMessage.Data)
                break;
                }
                return;
        }
        console.error(parsedMessage.Message)
        setError(parsedMessage.Message)
        
          }catch( ex){
            
            console.log(ex)
      }
    }
    
  },[message])
  const [joined, setJoined] = useState<boolean>(false);

  useEffect(() => {
    if (webSocket && webSocket.readyState === WebSocket.OPEN && !joined) {
      const message: QueryData = {
        event: 'room/join',
        room: 'Wheel',
        bet: null,
        betOn: null,
      };
      webSocket.send(JSON.stringify(message));
      setJoined(true);
    }

    if (webSocket && webSocket.readyState === WebSocket.CONNECTING && !joined) {
      webSocket.onopen = () => {
        const message: QueryData = {
          event: 'room/join',
          room: 'Wheel',
          bet: null,
          betOn: null,
        };
        webSocket.send(JSON.stringify(message));
        setJoined(true);
      };
    }
  }, [webSocket, joined]);



  const [BetOn, SetBetOn] = useState<string>("")
  const [error, setError] = useState<string>("")
  const SendBet = ()=>
  {
    setBet(0);
    if (webSocket && webSocket.readyState === WebSocket.OPEN ) {
  
    
        const message: QueryData = {
          event: 'room/bet',
          room: 'Wheel',
          bet: bet,
          betOn: BetOn,
        }
        webSocket.send(JSON.stringify(message));
      
      };
    
  
  }
  

  return (
    <div className={styles.Container}>
      <div className={styles.SubContainer}>
      <div className={styles.LineContaienr}>

        <div className={styles.Wheel}>
          <h1 className={styles.title}>Wheel</h1>
          <div style={{display:"flex", justifyContent:"space-between"}}>
            <Balance token = { localStorage.getItem('token')} HttpEndPoint={HttpEndPoint}   WsEndPoint={WsEndPoint} key={balanceKey}/>
            <button onClick={()=> {navigate("/Profile")} } style={{backgroundColor:"rgb(53,53,53)", color:"white", padding:"10px", cursor:"pointer", borderRadius:"5px"}}>Profile</button>
          </div>
            
          <CasinoWheelElem mustSpin = {mustSpin} prizeNumber = {prizeNumber} setMustSpin = {setMustSpin}/>
          
          {mustSpin ? (
            <div></div>
          ) : (
            <p className={styles.sub}>Won: {WonBetelem}</p>
          )}
        </div>
        <div className={styles.Panel}>
          
          <div className={styles.WheelControl}>
          {error ? <div style={{color:"red"}}>{error}</div> : null}

            <label htmlFor="bet" className={styles.label}>
              Your bet:
            </label>
            <input
              id="bet"
              type="number"


              value={bet || ''}
              onChange={(e) => setBet(e.target.value ? parseFloat(e.target.value) : undefined)}
              className={styles.input}
              placeholder="от 100"
              disabled={mustSpin}
            />
            
            <div className={styles.ButtonsContainer}>
              <button className={styles.ButtonRed}   disabled={mustSpin} onClick={() => SetBetOn("nechetn")}>2x</button>
              <button className={styles.ButtonGreen}  disabled={mustSpin} onClick={() => SetBetOn("single")}>15x </button>
              <button className={styles.ButtonBlack} disabled={mustSpin} onClick={() => SetBetOn("chetn")}>2x</button>
            </div>
            <button className={styles.SendBet}  disabled={mustSpin} onClick={SendBet}>Send bet</button>
          </div>
        </div>
          
        </div>
        <div className={styles.LineContaienr}>
        
  <div className={styles.line}>
    <div
      style={{ backgroundColor: "#521861", color: "white" }}
      className={styles.lineBetHeader}
    >
      2x
    </div>
    {BetsOfUsers.filter((User) => User.BetOn === "nechetn").length > 0 ? (
      BetsOfUsers.filter((User) => User.BetOn === "nechetn").map((User) => (
        <div className={styles.userline} key={User.UserID}>
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              gap: "5px",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <GetAvatar Id={User.UserID} HttpEndPoint={HttpEndPoint} WsEndPoint={WsEndPoint}  username={User.UserName} />
            <p>{User.UserName}</p>
          </div>
          <p>{User.Bet}</p>
        </div>
      ))
    ) : (
      <p style={{color:"gray"}}>Нет ставок</p>
    )}
  </div>
  <div className={styles.line}>
    <div style={{ backgroundColor: "White" }} className={styles.lineBetHeader}>
      15x
    </div>
    {BetsOfUsers.filter((User) => User.BetOn === "single").length > 0 ? (
      BetsOfUsers.filter((User) => User.BetOn === "single").map((User) => (
        <div className={styles.userline} key={User.UserID}>
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              gap: "5px",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <GetAvatar Id={User.UserID} HttpEndPoint={HttpEndPoint} WsEndPoint={WsEndPoint} username={User.UserName} />
            <p>{User.UserName}</p>
          </div>
          <p>{User.Bet}</p>
        </div>
      ))
    ) : (
      <p style={{color:"gray"}}>Нет ставок</p>
    )}
  </div>
  <div className={styles.line}>
    <div
      style={{ backgroundColor: "Black", color: "white" }}
      className={styles.lineBetHeader}
    >
      2x
    </div>
    {BetsOfUsers.filter((User) => User.BetOn === "chetn").length > 0 ? (
      BetsOfUsers.filter((User) => User.BetOn === "chetn").map((User) => (
        <div className={styles.userline} key={User.UserID}>
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              gap: "5px",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <GetAvatar Id={User.UserID} HttpEndPoint={HttpEndPoint} WsEndPoint={WsEndPoint} username={User.UserName} />
            <p>{User.UserName}</p>
          </div>
          <p>{User.Bet}</p>
        </div>
      ))
    ) : (
      <p style={{color:"gray"}}>Нет ставок</p>
    )}
  </div>
</div>
        
        <div className={styles.LineContaienr} style={{color:"white", display:"flex", flexDirection:"column"}}>
          <div className={styles.Subtitle}>Users in room</div>
          <UserAtRoomList  Clients = {users} HttpEndPoint={HttpEndPoint} WsEndPoint={WsEndPoint}/>
        </div>

      </div>
      </div>
  );
};

export default CasinoWheel;

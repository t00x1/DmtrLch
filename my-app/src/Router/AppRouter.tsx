import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from '../Home/Home';
import AuthPage from '../Auth/AuthPage';
import CasinoWheel from '../Wheel/CasinoWheel';
import ImageProfile from '../Profile/Profile';
import CasinoWheelElem from '../Wheel/CasinoWheelElem';

// Хук для WebSocket
const useWebSocket = (socketUrl: string) => {
  const [messages, setMessages] = useState<string[]>([]);
  const [isConnected, setIsConnected] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [socket, setSocket] = useState<WebSocket | null>(null);

  useEffect(() => {
    const newSocket = new WebSocket(socketUrl);
    setSocket(newSocket);

    newSocket.onopen = () => {
      setIsConnected(true);
      console.log('WebSocket connected');
    };

    newSocket.onmessage = (event) => {
      const newMessage = event.data;
      setMessages((prevMessages) => {
        const updatedMessages = [...prevMessages, newMessage];
        // Если сообщений больше 10, удаляем первое
        if (updatedMessages.length > 10) {
          updatedMessages.shift();  // Удаляет первый элемент
        }
        return updatedMessages;
      });
    };
    

    newSocket.onerror = (event) => {
      setError('WebSocket error occurred');
      console.error('WebSocket error:', event);
    };

    newSocket.onclose = (event) => {
      setIsConnected(false);
      console.log('WebSocket connection closed', event);
    };

    // Очистка WebSocket при размонтировании компонента
    return () => {
      newSocket.close();
    };
  }, [socketUrl]);

  return { messages, isConnected, error, socket };
};

// Компонент AppRouter
const AppRouter: React.FC = () => {
  const HttpEndPoint : string = process.env.REACT_APP_API_HTTP_URL || "http://localhost:5019";
  const WsEndPoint : string = process.env.REACT_APP_API_WS_URL || "ws://localhost:5019";
  console.log(HttpEndPoint)
  console.log(WsEndPoint)
  const Token: string | null = localStorage.getItem('token');
  const socketUrl = `${WsEndPoint}/ws?token=${Token}`;

  const { messages, isConnected, error, socket } = useWebSocket(socketUrl);

  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home  />} />
        <Route path="/auth" element={<AuthPage HttpEndPoint={HttpEndPoint} WsEndPoint= {WsEndPoint}  />} />
        <Route path="/wheel" element={<CasinoWheel  webSocket={socket} message= {messages} HttpEndPoint={HttpEndPoint} WsEndPoint= {WsEndPoint}/>} />
        <Route path="/Profile" element={<ImageProfile HttpEndPoint={HttpEndPoint} WsEndPoint= {WsEndPoint} />} />
      
      </Routes>
    </Router>
  );
};

export default AppRouter;

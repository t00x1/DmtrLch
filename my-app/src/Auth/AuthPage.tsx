import React, { useEffect, useState } from 'react'; 
import LoginPage from './Login/LoginPage';
import styles from './AuthPage.module.css';
import RegistrationForm from './Register/Register';
import { responseStatus, apiResponse, userDTO } from '../Domain/types';
import EmailConfirm from './Register/EmailConfirm/EmailConfirm';
interface AuthPageProps {
  WsEndPoint: string;
  HttpEndPoint: string;
}
const AuthPage: React.FC<AuthPageProps> = ({ WsEndPoint,HttpEndPoint }) => {
  const [currentWindow, setCurrentWindow] = useState<string>('register')
  const [User, setUser] = useState<userDTO>({
    idOfUser: '',
    email: '',
    userName: '',
    name: '',
    surname: '',
    patronymic: '',
    password: '',
    avatar: '',
    token: '',
});

  const handleValueChange = (newValue: string) => {
    setCurrentWindow(newValue);
  };
  const handleUserChange = (newValue: userDTO) => {
    setUser(newValue);
  };
  useEffect(()=>{console.log(User)},[User])
  return (
    <div className={styles.container}>
      {currentWindow === 'register' ? 
      (
        <RegistrationForm HttpEndPoint = {HttpEndPoint} currentWindow = {handleValueChange} user = {handleUserChange}/>
      ) : currentWindow === 'login' ?
      (
        <LoginPage HttpEndPoint = {HttpEndPoint} currentWindow = {handleValueChange}/>
      ) : (<EmailConfirm HttpEndPoint = {HttpEndPoint} currentWindow = {handleValueChange} user = {User}/>)
      }
    </div>
  );
};

export default AuthPage;

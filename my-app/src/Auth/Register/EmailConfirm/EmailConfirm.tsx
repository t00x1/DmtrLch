import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { responseStatus, apiResponse, userDTO } from '../../../Domain/types';
import styles from '../../AuthPage.module.css';

// Типизация пропсов компонента LoginPage
interface EmailConfirmrops {
  HttpEndPoint: string;
  currentWindow: (newValue: string) => void; 
    user: userDTO;
}

const EmailConfirm: React.FC<EmailConfirmrops> = ({ HttpEndPoint, currentWindow, user }) => {

  const [error, setError] = useState<string>('');
  const [Code, setCode] = useState<string>('');

  const navigate = useNavigate(); 

  const handleSendCodeSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
      try {
        const response = await fetch(`${HttpEndPoint}/Client/emailsendcode`, { 
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            Accept: '*/*',
          },
          body: JSON.stringify({email: user.email}),
        });

        const responseData = await response.json();
        if (response.status === 200) {
          
        } 
        setError(responseData.message || 'Произошла ошибка.');
    } catch (error) {
        setError('Ошибка сети. Попробуйте позже.');
    }
    
};
const handleConfirmSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    try {
        const response = await fetch(`${HttpEndPoint}/Client/emailconfirmcode`, { 
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Accept: '*/*',
            },
            body: JSON.stringify({email: user.email,username:user.userName, token: Code}),
        });
        
        const responseData = await response.json();
        if (response.status === 200) {
            
           
            currentWindow('login');
        } 
          setError(responseData.message || 'Произошла ошибка.');
      } catch (error) {
        setError('Ошибка сети. Попробуйте позже.');
      }
    
  };
  

  return (
    <div className={styles.SubContainer}>
     
      <div className={styles.registerSide}>
        <div className={styles.form}>
          <h1 className={styles.title}>Email Confirm</h1> 
        <h2 className={styles.Subtitle}>Send code and check your email</h2>
        <h2 className={styles.Subtitle} style={{color:"gray"}}>{user.email}</h2>
        {error && <div className={styles.error}>{error}</div>}
          
        <div className={styles.inputGroupSecond} >
            <input
              id="Code"
              type="text"
              className={styles.input}
              value={Code}
              onChange={(e) => setCode(e.target.value)}
              placeholder="Enter your code"
            />
            <button type="submit" onClick={handleSendCodeSubmit} style={{marginLeft:"10px"}}className={styles.submitButton} >Send Code</button>

          </div>
          <button type="submit" onClick={handleConfirmSubmit} className={styles.submitButton}>Login</button>
        </div>
      </div>
    </div>
  );
};

export default EmailConfirm;

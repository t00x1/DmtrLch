import React, { useState, useEffect } from 'react'; 
import { useNavigate, Link } from 'react-router-dom';
import { responseStatus, apiResponse, userDTO } from '../../Domain/types';
import styles from '../AuthPage.module.css';

interface RegisterPageProps {
  HttpEndPoint: string; 
  currentWindow: (newValue: string) => void; 
  user: (newValue: userDTO) => void; 
}

const RegisterPage: React.FC<RegisterPageProps> = ({ HttpEndPoint, currentWindow, user }) => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [confirmPassword, setConfirmPassword] = useState<string>('');
  const navigate = useNavigate();
  const [email, setEmail] = useState<string>('');
  const [name, setName] = useState<string>('');
  const [surname, setSurname] = useState<string>('');
  const [patronymic, setPatronymic] = useState<string>('');
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

 

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!username || !password || !confirmPassword || !email || !name || !surname) {
      setErrorMessage('Please fill in all fields.');
      return;
    }
  
    if (password !== confirmPassword) {
      setErrorMessage('Passwords do not match.');
      return;
    }
  
    try {
      const response = await fetch(`${HttpEndPoint}/Client/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Accept: '*/*',
        },
        body: JSON.stringify({
          email,
          userName: username,
          name,
          surname,
          patronymic,
          password,
        }),
      });
  
      const data: apiResponse<userDTO> = await response.json();
      console.log(data)
      
      if (data.status === responseStatus.ok && data.data !== null) {
        currentWindow('confirm');
        user(data.data);
        
      
      } 
      setErrorMessage(data.message || 'Registration error occurred.');
      
  
    } catch (error) {
      setErrorMessage('Network error. Please try again later.');
    }
  };
  

 
  const ChangeWindow = () => 
  {
    currentWindow('login');
  }

  return (
    <div className={styles.SubContainer}>
      <div className={styles.WelocomeSide}>
        <h2 className={styles.title}>Welcome</h2>
        <h2 className={styles.Subtitle}>to Randomizer Bet</h2>
        <a
          className={styles.Link}
          onClick={ChangeWindow}
          >
        
          Already have an account?
        </a>
        {errorMessage && <div className={styles.error}>{errorMessage}</div>}
      </div>
      <div className={styles.registerSide}>
        <form className={styles.form} onSubmit={handleSubmit}>
          <div className={styles.inputGroup}>
            <h2 className={styles.title}>Registration</h2>
            <input
              id="username"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className={styles.input}
              placeholder="Enter username"
            />
          </div>
          <div className={styles.inputGroup}>
            <input
              id="email"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className={styles.input}
              placeholder="Enter your email"
            />
          </div>
          <div className={styles.inputGroup}>
            <input
              id="name"
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className={styles.input}
              placeholder="Enter first name"
            />
          </div>
          <div className={styles.inputGroup}>
            <input
              id="surname"
              type="text"
              value={surname}
              onChange={(e) => setSurname(e.target.value)}
              className={styles.input}
              placeholder="Enter last name"
            />
          </div>
          <div className={styles.inputGroup}>
            <input
              id="patronymic"
              type="text"
              value={patronymic}
              onChange={(e) => setPatronymic(e.target.value)}
              className={styles.input}
              placeholder="Enter patronymic"
            />
          </div>
          <div className={styles.inputGroup}>
            <input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className={styles.input}
              placeholder="Enter password"
            />
          </div>
          <div className={styles.inputGroup}>
            <input
              id="confirmPassword"
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              className={styles.input}
              placeholder="Confirm password"
            />
          </div>
          <button type="submit" className={styles.submitButton}>
            Register
          </button>

        </form>
      </div>
    </div>
  );
};

export default RegisterPage;  
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from '../AuthPage.module.css';

// Типизация пропсов компонента LoginPage
interface LoginPageProps {
  HttpEndPoint: string;
  currentWindow: (newValue: string) => void; 
}

// Типизация объекта, возвращаемого YaAuthSuggest.init
interface TokenData {
  token: string;
  // Добавьте другие свойства, если они есть
}

// Типизация объекта ошибки
interface ErrorData {
  message: string;
  // Добавьте другие свойства, если они есть
}

const LoginPage: React.FC<LoginPageProps> = ({ HttpEndPoint, currentWindow }) => {
  const [login, setLogin] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string>('');
  const navigate = useNavigate(); // Получаем навигацию для переходов

  // useEffect(() => {
  //   if (window.YaAuthSuggest) {
  //     window.YaAuthSuggest.init(
  //       {
  //         client_id: "eea3b8202a1f41e2bfdca2457c3d19cd",
  //         response_type: "token"
  //       },
  //       "https://oauth.yandex.ru",
  //       {
  //         view: "button",
  //         parentId: "buttonContainerId",
  //         buttonSize: 'm',
  //         buttonView: 'main',
  //         buttonTheme: 'light',
  //         buttonBorderRadius: "0",
  //         buttonIcon: 'ya',
  //       }
  //     )
  //     .then((result: { handler: () => Promise<TokenData> }) => result.handler())
  //     .then((data: TokenData) => console.log('Сообщение с токеном', data))
  //     .catch((error: ErrorData) => console.log('Обработка ошибки', error))
  //   }
  // }, []);
  

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (login === '' || password === '') {
      setError('Заполните все поля');
    } else {
      setError('');
      try {
        const response = await fetch(`${HttpEndPoint}/Client/login`, { 
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            Accept: '*/*',
          },
          body: JSON.stringify({
            userName: login,
            password: password,
          }),
        });
        
        const responseData = await response.json();
        if (response.status === 200) {
          const token = responseData.data.token; 
          console.log(' токен', token); 

          localStorage.setItem('token', token);
          navigate('/wheel');
        } else {
          setError(responseData.message || 'Произошла ошибка авторизации.');
        }
      } catch (error) {
        setError('Ошибка сети. Попробуйте позже.');
      }
    }
  };

  const ChangeWindow = () => {
    currentWindow('register');
  };

  return (
    <div className={styles.SubContainer}>
      <div className={styles.WelocomeSide}>
        <h2 className={styles.title}>Welcome</h2>
        <h2 className={styles.Subtitle}>to Randomizer Bet</h2>
        <a className={styles.Link} onClick={ChangeWindow}>
          Already have an account?
        </a>
        {error && <div className={styles.error}>{error}</div>}
      </div>
      <div className={styles.registerSide}>
        <form onSubmit={handleSubmit} className={styles.form}>
          <h1 className={styles.title}>Authorization</h1> 
          <div className={styles.inputGroup}>
            <input
              id="UserName"
              type="text"
              className={styles.input}
              value={login}
              onChange={(e) => setLogin(e.target.value)}
              placeholder="Enter your login"
            />
          </div>
          <div className={styles.inputGroup}>
            <input
              id="password"
              type="password"
              className={styles.input}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Enter your password"
            />
          </div>
          <button type="submit" className={styles.submitButton}>Login</button>
          {/* <div id="buttonContainerId" className={styles.buttonContainer}></div> */}
        </form>
      </div>
    </div>
  );
};

export default LoginPage;

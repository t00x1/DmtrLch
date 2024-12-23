import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { responseStatus, apiResponse, userDTO } from '../Domain/types';
import styles from "./styles.module.css";
import { Console } from 'console';
import Balance from '../Domain/Balance/Balance';

interface ImageProfileProps {
  HttpEndPoint: string; // URL до API
  WsEndPoint: string; // URL до API
}

const ImageProfile: React.FC<ImageProfileProps> = ({ WsEndPoint,  HttpEndPoint}) => {
  const location = useLocation();
  const [imageUrl, setImageUrl] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [errorCoupon, seterrorCoupon] = useState<string | null>(null);
  const [uploading, setUploading] = useState<boolean>(false);

  const getQueryParam = (param: string) => {
    const params = new URLSearchParams(location.search);
    return params.get(param);
  };
  const [balanceKey, setBalanceKey] = useState<number>(0);
  const userName = getQueryParam('username');

  const [user, setUser] = useState<userDTO>({
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

  const fetchImage = async () => {
    const token = localStorage.getItem('token');

    if (!token) {
      setError('Токен авторизации не найден.');
      return;
    }

    try {
      const response = await fetch(`${HttpEndPoint}/Client/Profile1?userName=${userName}`, {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': '*/*',
        },
      });

      if (response.ok) {
        const blob = await response.blob();
        const objectUrl = URL.createObjectURL(blob);
        setImageUrl(objectUrl);
        console.log(blob.size);
        if (blob.size < 45) {
          
          setImageUrl(null);
          return;
        }
      } 
      
      
    } catch (err) {
      setError('Произошла ошибка при загрузке изображения.');
      console.error(err);
      setImageUrl(null);
    }
  };

  const fetchUserData = async () => {
    const token = localStorage.getItem('token');

    if (!token) {
      setError('Токен авторизации не найден.');
      return;
    }

    try {
      const response = await fetch(`${HttpEndPoint}/Client/Profile?userName=${userName}`, {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
          Accept: '*/*',
        },
      });

      const data: apiResponse<userDTO> = await response.json();
      console.log(data);

      if (data.status === responseStatus.ok && data.data) {
        console.log(data)
        setUser(data.data);
      } else {
        if(!data.isSuccess){

          setError(data.message  || 'Ошибка при получении данных пользователя.');
        }
      }
    } catch (error) {
      setError('Произошла ошибка при получении данных пользователя.');
      console.error(error);
    }
  };
  const [Code, setCode] = useState<string>("");
  const handleCouponSend = async ()=>
  {
    const token = localStorage.getItem('token');

    if (!token) {
      setError('Токен авторизации не найден.');
      return;
    }

    try {
      console.log(Code);
      const response = await fetch(`${HttpEndPoint}/Client/ActivateCupon?cupon=${Code}`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
          Accept: '*/*',
        },
      });

      const data: apiResponse<userDTO> = await response.json();
      console.log(data);

      if (data.status === responseStatus.ok && data.data) {
        console.log(data)
        setBalanceKey((prevKey) => prevKey + 1);
       
      }else{
        seterrorCoupon(data.message)
      }
  }
  catch(ex){console.log(ex)}
  
}
  const handleImageUpload = async (file: File) => {
    const token = localStorage.getItem('token');
    if (!token) {
      setError('Токен авторизации не найден.');
      return;
    }

    const formData = new FormData();
    formData.append('file', file);

    setUploading(true);

    try {
      const response = await fetch(`${HttpEndPoint}/Client/upload`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
        },
        body: formData,
      });

      if (response.ok) {
        setError(null);
        await fetchImage();
      } else {
        setError(`Ошибка загрузки файла: ${response.statusText}`);
      }
    } catch (err) {
      setError('Произошла ошибка при загрузке файла.');
      console.error(err);
    } finally {
      setUploading(false);
    }
  };

  useEffect(() => {
    fetchImage();
    fetchUserData();

    return () => {
      if (imageUrl) {

        URL.revokeObjectURL(imageUrl);
      }
    };
  }, [HttpEndPoint, userName]);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files[0]) {
      handleImageUpload(event.target.files[0]);
    }
  };

  return (
    <div className={styles.container}>
      {error != null ? (<div>error && <p className={styles.error}>{error}</p></div>):(<div className={styles.SubContainer}>
        <div style={{gap:"15px"}} className={styles.Column}>
          <div className={styles.block}>
            {imageUrl ? (
              <div>
                  <img
                    src={imageUrl}
                    alt={user.userName?.[0] || "User"}
                    onClick={() => document.getElementById('fileInput')?.click()}
                    className={styles.Picture}
                    style={{cursor: 'pointer' }}
                  />

              </div>
            ) : (
              <div
                className={styles.Fallback}
                onClick={() => document.getElementById('fileInput')?.click()}
                style={{ cursor: 'pointer' }}
              >
                {user.userName && user.userName[0] ? user.userName[0].toUpperCase() : "U"}
              </div>
            )}
            <input
              type="file"
              id="fileInput"
              style={{ display: 'none' }}
              onChange={handleFileChange}
              accept="image/*"
            />
            <div className={styles.textsContainer}>
              <h1 className={styles.title}>{user.userName}</h1>
              <h2 className={styles.Under}>email: {user.email}</h2>
              {uploading && <p>Загрузка...</p>}
            
             
            </div>
                <Balance token = { localStorage.getItem('token')} HttpEndPoint={HttpEndPoint} WsEndPoint= {WsEndPoint} key={balanceKey}/>
          </div>
            <div style= {{flexDirection:"column"}}className={styles.block}>
              <div className={styles.label}>Enter the code:</div>
              <div className={styles.inputGroupSecond}>
                <input
                  id="Code"
                  type="text"
                  className={styles.input}
                  value={Code}
                  onChange={(e) => setCode(e.target.value)}
                  placeholder="Enter your code"
                />
                <button type="submit"  onClick = {handleCouponSend}style={{marginLeft:"10px"}}className={styles.submitButton} >Send Code</button>
            </div>
              <div style={{color:"red"}}>{errorCoupon}</div>
             
            </div>
         
        </div>
      </div>)}
    </div>
  );
};

export default ImageProfile;

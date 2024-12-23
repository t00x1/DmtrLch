import React, { useState, useEffect } from 'react';

interface GetAvatarProps {
  Id: string;
  WsEndPoint: string;
  HttpEndPoint: string;
  username: string;
}

const GetAvatar: React.FC<GetAvatarProps> = ({ Id, WsEndPoint,HttpEndPoint, username }) => {
  const [imageUrl, setImageUrl] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [uploading, setUploading] = useState<boolean>(false);

  useEffect(() => {
    fetchImage();

    return () => {
      if (imageUrl) {
        URL.revokeObjectURL(imageUrl);
      }
    };
  }, [HttpEndPoint, username]);

  const fetchImage = async () => {
    const token = localStorage.getItem('token');

    if (!token) {
      setError('Токен авторизации не найден.');
      return;
    }

    try {
      setUploading(true);
      const response = await fetch(`${HttpEndPoint}/Client/Profile1?userName=${Id}`, {
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

        // Если размер изображения слишком маленький, не показываем его
        if (blob.size < 45) {
          setImageUrl(null);
        }
      } else {
        setError('Не удалось загрузить изображение.');
      }
    } catch (err) {
      setError('Произошла ошибка при загрузке изображения.');
      console.error(err);
      setImageUrl(null);
    } finally {
      setUploading(false);
    }
  };

  return (
    <div>
      {uploading && <p>Загрузка...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {imageUrl ? (
        <div>
          <img
            src={imageUrl}
            alt={username?.[0] || 'User'}
            style={{width: 50, height: 50, borderRadius: '50%' }}
          />
        </div>
      ) : (
        <div
          style={{
            width: 50,
            height: 50,
            borderRadius: '50%',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            backgroundColor: '#ccc',
            cursor: 'pointer',
          }}
        >
          {username && username[0] ? username[0].toUpperCase() : 'U'}
        </div>
      )}
    </div>
  );
};

export default GetAvatar;

import React, { useEffect, useState } from 'react';
import { responseStatus, apiResponse, userDTO } from '../../Domain/types';

interface BalanceProps {
    token: string | null;
    HttpEndPoint: string;
    WsEndPoint: string;
    key: number;
}

const Balance: React.FC<BalanceProps> = ({ token, HttpEndPoint, WsEndPoint,key }) => {
    const [userData, setUserData] = useState<userDTO | null>(null);
    const [error, setError] = useState<string | null>(null);

    const fetchUserData = async () => {
        try {
            const response = await fetch(`${HttpEndPoint}/Client/GetBalance`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json',
                    Accept: '*/*',
                },
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data: apiResponse<userDTO> = await response.json();
            console.log(response)
            if (data.status === responseStatus.ok) {
                setUserData(data.data);
            } else {
                setError(data.message || 'Unknown error');
            }
        } catch (error) {
            setError((error as Error).message);
        }
    };

    useEffect(() => {
        fetchUserData();
    }, [key]);

    return (
        <div>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {userData && (
                <div style={{backgroundColor:"rgb(50,50,50)", padding:"10px", borderRadius:"10px"}}>
                    
                    <p style={{color:"white"}}>Balance: {userData.balance}</p>
                </div>
            )}
        </div>
    );
};

export default Balance;
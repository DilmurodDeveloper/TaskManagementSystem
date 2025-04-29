import React, { useState, useEffect } from 'react';
import { getUserInfo } from '../api/user';
import { getProjects } from '../api/project'; 

const DashboardPage = () => {
    const [userInfo, setUserInfo] = useState(null);
    const [projects, setProjects] = useState([]);

    useEffect(() => {
        const fetchUserInfo = async () => {
            try {
                const response = await getUserInfo();
                setUserInfo(response);
            } catch (error) {
                console.error('Foydalanuvchi ma\'lumotlari olinmadi', error);
            }
        };

        const fetchProjects = async () => {
            try {
                const response = await getProjects();
                setProjects(response);
            } catch (error) {
                console.error('Loyihalar olinmadi', error);
            }
        };

        fetchUserInfo();
        fetchProjects();
    }, []); 

    return (
        <div>
            <h1>Dashboard</h1>
            <h2>Foydalanuvchi Ma'lumotlari</h2>
            {userInfo ? (
                <div>
                    <p>Ism: {userInfo.username}</p>
                    <p>Email: {userInfo.email}</p>
                </div>
            ) : (
                <p>Foydalanuvchi ma'lumotlari yuklanmoqda...</p>
            )}

            <h2>Loyihalar</h2>
            {projects.length > 0 ? (
                <ul>
                    {projects.map(project => (
                        <li key={project.id}>{project.name}</li>
                    ))}
                </ul>
            ) : (
                <p>Loyihalar yuklanmoqda...</p>
            )}
        </div>
    );
};

export default DashboardPage;

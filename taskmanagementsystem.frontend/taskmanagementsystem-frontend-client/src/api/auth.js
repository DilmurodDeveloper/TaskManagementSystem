import axios from 'axios';

const API_URL = 'https://localhost:7212/api/auth';

export const registerUser = async (registerData) => {
    return await axios.post(`${API_URL}/register`, registerData);
};

export const loginUser = async (loginData) => {
    const response = await axios.post(`${API_URL}/login`, loginData);
    if (response.data && response.data.token) {
        localStorage.setItem('token', response.data.token);
    }
    return response;
};

export const logoutUser = () => {
    localStorage.removeItem('token');
};

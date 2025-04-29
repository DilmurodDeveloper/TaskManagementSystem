import axios from 'axios';

const API_URL = 'https://localhost:7212/api/User'; 

export const getUserInfo = async () => {
    try {
        const response = await axios.get(`${API_URL}/me`);
        return response.data;
    } catch (error) {
        throw error;
    }
};

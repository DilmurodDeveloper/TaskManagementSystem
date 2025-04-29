import axiosInstance from './axiosInstance';

const API_URL = 'https://localhost:7212/api/User'; 

export const getUserInfo = async () => {
    try {
        const response = await axiosInstance.get(`${API_URL}/me`);
        return response.data;
    } catch (error) {
        throw error;
    }
};

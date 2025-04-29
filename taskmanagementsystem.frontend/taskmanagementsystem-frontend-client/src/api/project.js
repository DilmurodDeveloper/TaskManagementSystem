import axios from 'axios';

const API_URL = 'https://localhost:7212/api/Project'; 

export const getProjects = async () => {
    try {
        const response = await axios.get(API_URL); 
        return response.data;
    } catch (error) {
        throw error;
    }
};

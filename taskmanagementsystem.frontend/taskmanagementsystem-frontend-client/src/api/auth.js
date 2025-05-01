import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

export const login = async (data) => {
    try {
        const response = await axios.post("/api/auth/login", data);
        console.log(response);
    } catch (error) {
        console.error(error);
    }
};

export const register = async (userData) => {
    const response = await axios.post(`${API_URL}/api/auth/register`, userData);
    return response.data;
};

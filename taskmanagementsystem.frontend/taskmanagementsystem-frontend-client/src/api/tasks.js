import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

export const getTasks = async (projectId, token) => {
    const response = await axios.get(`/api/Task?projectId=${projectId}`, {
        baseURL: API_URL,
        headers: { Authorization: `Bearer ${token}` },
    });
    return response.data;
};

export const createTask = async (task, token) => {
    const response = await axios.post("/api/Task", task, {
        baseURL: API_URL,
        headers: { Authorization: `Bearer ${token}` },
    });
    return response.data;
};

export const updateTask = async (id, task, token) => {
    const response = await axios.put(`/api/Task/${id}`, task, {
        baseURL: API_URL,
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });
    return response.data;
};

export const deleteTask = async (id, token) => {
    await axios.delete(`/api/Task/${id}`, {
        baseURL: API_URL,
        headers: { Authorization: `Bearer ${token}` },
    });
};

import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

export const getProjects = async (token) => {
    const response = await axios.get("/api/Project", {
        baseURL: API_URL,
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

export const createProject = async (project, token) => {
    await axios.post("/api/Project", project, {
        baseURL: API_URL,
        headers: { Authorization: `Bearer ${token}` }
    });
};

export const deleteProject = async (id, token) => {
    await axios.delete(`/api/Project/${id}`, {
        baseURL: API_URL,
        headers: { Authorization: `Bearer ${token}` }
    });
};

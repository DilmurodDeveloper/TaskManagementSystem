import axiosInstance from './axiosInstance';

const BASE = '/Project';

export const getProjects = async () => {
    const response = await axiosInstance.get(BASE);
    return response.data;
};

export const createProject = async (projectData) => {
    const response = await axiosInstance.post(BASE, projectData);
    return response.data;
};

export const updateProject = async (id, projectData) => {
    const response = await axiosInstance.put(`${BASE}/${id}`, projectData);
    return response.data;
};

export const deleteProject = async (id) => {
    await axiosInstance.delete(`${BASE}/${id}`);
};

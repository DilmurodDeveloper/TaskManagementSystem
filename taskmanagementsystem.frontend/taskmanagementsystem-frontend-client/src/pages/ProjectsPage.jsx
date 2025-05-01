import React, { useEffect, useState, useContext } from "react";
import { getProjects, createProject, deleteProject } from "../api/project";
import ProjectList from "../components/Projects/ProjectList";
import ProjectForm from "../components/Projects/ProjectForm";
import { AuthContext } from "../contexts/AuthContext";
import axios from "axios";

export default function ProjectsPage() {
    const [projects, setProjects] = useState([]);
    const { token } = useContext(AuthContext);

    useEffect(() => {
        fetchProjects();
    }, []);

    const fetchProjects = async () => {
        try {
            const response = await axios.get("/api/Project", {
                baseURL: process.env.REACT_APP_API_URL,
                headers: { Authorization: `Bearer ${token}` },
            });

            const data = response.data;

            const projectList = Array.isArray(data)
                ? data
                : Array.isArray(data.projects)
                    ? data.projects
                    : [];

            setProjects(projectList);
        } catch (err) {
            console.error("Error fetching projects:", err);
            setProjects([]);
        }
    };

    const handleCreate = async (proj) => {
        try {
            await axios.post("/api/Project", proj, {
                baseURL: process.env.REACT_APP_API_URL,
                headers: { Authorization: `Bearer ${token}` },
            });
            fetchProjects();
        } catch (err) {
            console.error("Error creating project:", err);
        }
    };

    const handleDelete = async (id) => {
        try {
            await axios.delete(`/api/Project/${id}`, {
                baseURL: process.env.REACT_APP_API_URL,
                headers: { Authorization: `Bearer ${token}` },
            });
            setProjects((prev) => prev.filter((p) => p.id !== id));
        } catch (err) {
            console.error("Error deleting project:", err);
        }
    };

    return (
        <div>
            <h2>Loyihalar ro'yxati</h2>
            <ProjectForm onSubmit={handleCreate} />
            {projects.length > 0 ? (
                <ProjectList projects={projects} onDelete={handleDelete} />
            ) : (
                <p>Hozircha loyiha yo'q.</p>
            )}
        </div>
    );
}

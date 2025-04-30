import React, { useEffect, useState } from 'react';
import { getProjects, deleteProject } from '../api/project';
import { useNavigate } from 'react-router-dom';

const ProjectsPage = () => {
    const [projects, setProjects] = useState([]);
    const [error, setError] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        fetchProjects();
    }, []);

    const fetchProjects = async () => {
        try {
            const data = await getProjects();
            setProjects(data);
        } catch (err) {
            setError('Loyihalarni olishda xatolik yuz berdi');
            console.error(err);
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm('Haqiqatan ham o‘chirmoqchimisiz?')) return;
        try {
            await deleteProject(id);
            setProjects(p => p.filter(proj => proj.id !== id));
        } catch (err) {
            setError('Loyihani o‘chirishda xatolik');
            console.error(err);
        }
    };

    const handleEdit = (id) => {
        navigate(`/projects/edit/${id}`);
    };

    return (
        <div>
            <h1>Loyihalar ro‘yxati</h1>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <button onClick={() => navigate('/projects/new')}>
                Yangi loyiha qo‘shish
            </button>
            <ul>
                {projects.map(proj => (
                    <li key={proj.id} style={{ margin: '0.5em 0' }}>
                        <strong>{proj.name}</strong> — {proj.description}
                        <button onClick={() => handleEdit(proj.id)} style={{ marginLeft: 8 }}>
                            Tahrirlash
                        </button>
                        <button onClick={() => handleDelete(proj.id)} style={{ marginLeft: 4 }}>
                            O‘chirish
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ProjectsPage;

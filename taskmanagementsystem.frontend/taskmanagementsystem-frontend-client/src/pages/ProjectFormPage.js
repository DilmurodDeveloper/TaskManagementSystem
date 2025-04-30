import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import axios from '../api/axiosInstance'; 

const ProjectFormPage = () => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [error, setError] = useState('');
    const [isEditing, setIsEditing] = useState(false);
    const { id } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        if (id) {
            setIsEditing(true);
            fetchProject(id);
        }
    }, [id]);

    const fetchProject = async (id) => {
        try {
            const response = await axios.get(`/Project/${id}`);
            setName(response.data.name);
            setDescription(response.data.description);
        } catch (err) {
            setError('Loyihani olishda xatolik yuz berdi');
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const projectData = { name, description };

        try {
            if (isEditing) {
                await axios.put(`/Project/${id}`, projectData);  
            } else {
                await axios.post('/Project', projectData);       
            }
            navigate('/projects');
        } catch (err) {
            setError('Loyihani saqlashda xatolik');
        }
    };

    return (
        <div>
            <h1>{isEditing ? 'Loyihani tahrirlash' : 'Yangi loyiha yaratish'}</h1>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Loyihaning nomi:</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Loyihaning tavsifi:</label>
                    <textarea
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        required
                    ></textarea>
                </div>
                <button type="submit">{isEditing ? 'Tahrirni saqlash' : 'Loyihani yaratish'}</button>
            </form>
        </div>
    );
};

export default ProjectFormPage;

import React, { useState, useEffect } from 'react';

function Dashboard({ user }) {
    const [projects, setProjects] = useState([]);

    useEffect(() => {
        const fetchProjects = async () => {
            const response = await fetch('https://localhost:7212/swagger/index.html', {
                headers: { 'Authorization': `Bearer ${user}` },
            });

            const data = await response.json();
            if (response.ok) {
                setProjects(data);
            }
        };

        fetchProjects();
    }, [user]);

    return (
        <div>
            <h2>Dashboard</h2>
            <h3>Your Projects</h3>
            <ul>
                {projects.map(project => (
                    <li key={project.id}>{project.name}</li>
                ))}
            </ul>
            <button onClick={() => alert('Create a new project')}>Create Project</button>
        </div>
    );
}

export default Dashboard;

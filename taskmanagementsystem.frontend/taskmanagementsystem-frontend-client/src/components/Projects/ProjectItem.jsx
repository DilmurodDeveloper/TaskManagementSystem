import React from "react";

export default function ProjectItem({ project, onDelete }) {
    return (
        <div style={{ border: "1px solid #ccc", padding: 8, margin: 4 }}>
            <h3>{project.name}</h3>
            <p>{project.description}</p>
            <p>
                {new Date(project.startDate).toLocaleDateString()} - {" "}
                {new Date(project.endDate).toLocaleDateString()}
            </p>
            <button onClick={() => onDelete(project.id)}>O'chirish</button>
        </div>
    );
}

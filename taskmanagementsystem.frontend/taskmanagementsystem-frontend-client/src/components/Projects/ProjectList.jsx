import React from "react";
import ProjectItem from "./ProjectItem";

export default function ProjectList({ projects, onDelete }) {
    return (
        <div>
            {projects.map((p) => (
                <ProjectItem key={p.id} project={p} onDelete={onDelete} />
            ))}
        </div>
    );
}

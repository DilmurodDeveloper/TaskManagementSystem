import React, { useState } from "react";

export default function ProjectForm({ onSubmit }) {
    const [form, setForm] = useState({
        name: "",
        description: "",
        startDate: "",
        endDate: "",
        userId: 1, 
    });

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(form);
        setForm({ name: "", description: "", startDate: "", endDate: "", userId: 1 });
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Yangi loyiha qo'shish</h2>
            <input name="name" placeholder="Nom" value={form.name} onChange={handleChange} required />
            <textarea
                name="description"
                placeholder="Tavsif"
                value={form.description}
                onChange={handleChange}
            />
            <input
                name="startDate"
                type="date"
                value={form.startDate}
                onChange={handleChange}
                required
            />
            <input
                name="endDate"
                type="date"
                value={form.endDate}
                onChange={handleChange}
                required
            />
            <button type="submit">Saqlash</button>
        </form>
    );
}

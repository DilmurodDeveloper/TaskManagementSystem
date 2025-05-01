import React, { useEffect, useState } from "react";

export default function TaskForm({ onSubmit, projectId, initialValues = {}, onCancel }) {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [dueDate, setDueDate] = useState("");

    useEffect(() => {
        setTitle(initialValues.title || "");
        setDescription(initialValues.description || "");
        setDueDate(initialValues.dueDate ? initialValues.dueDate.substring(0, 10) : "");
    }, [initialValues]);

    const handleSubmit = (e) => {
        e.preventDefault();

        onSubmit({
            ...initialValues,
            title,
            description,
            dueDate,
            projectId,
        });

        if (!initialValues.id) {
            setTitle("");
            setDescription("");
            setDueDate("");
        }
    };

    return (
        <form onSubmit={handleSubmit} style={{ marginBottom: "1rem" }}>
            <input
                type="text"
                placeholder="Sarlavha"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <input
                type="text"
                placeholder="Tavsif"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                required
            />
            <input
                type="date"
                value={dueDate}
                onChange={(e) => setDueDate(e.target.value)}
                required
            />
            <button type="submit">
                {initialValues.id ? "Yangilash" : "Qo'shish"}
            </button>
            {initialValues.id && onCancel && (
                <button type="button" onClick={onCancel}>
                    Bekor qilish
                </button>
            )}
        </form>
    );
}

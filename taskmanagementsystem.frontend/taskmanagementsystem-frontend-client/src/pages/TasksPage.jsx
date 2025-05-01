import React, { useEffect, useState, useContext, useCallback } from "react";
import { getTasks, createTask, updateTask, deleteTask } from "../api/tasks";
import TaskForm from "../components/Tasks/TaskForm";
import { AuthContext } from "../contexts/AuthContext";
import { useParams } from "react-router-dom";

export default function TasksPage() {
    const { projectId } = useParams();
    const { token } = useContext(AuthContext);
    const [tasks, setTasks] = useState([]);
    const [editingTask, setEditingTask] = useState(null);

    const fetchTasks = useCallback(async () => {
        if (!projectId) return;
        try {
            const data = await getTasks(projectId, token);
            setTasks(data);
        } catch (err) {
            console.error("Error fetching tasks", err);
        }
    }, [projectId, token]);

    useEffect(() => {
        fetchTasks();
    }, [fetchTasks]);

    const handleCreate = async (task) => {
        try {
            await createTask(task, token);
            fetchTasks();
        } catch (err) {
            console.error("Error creating task", err);
        }
    };

    const handleUpdate = async (task) => {
        try {
            await updateTask(task.id, task, token);
            setEditingTask(null);
            fetchTasks();
        } catch (err) {
            console.error("Error updating task", err);
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteTask(id, token);
            setTasks((prev) => prev.filter((t) => t.id !== id));
        } catch (err) {
            console.error("Error deleting task", err);
        }
    };

    const handleToggleComplete = async (task) => {
        try {
            const updated = { ...task, isCompleted: !task.isCompleted };
            await updateTask(task.id, updated, token);
            fetchTasks();
        } catch (err) {
            console.error("Error toggling task", err);
        }
    };

    const handleSubmit = (task) => {
        if (editingTask) {
            handleUpdate(task);
        } else {
            handleCreate(task);
        }
    };

    return (
        <div>
            <h2>Vazifalar</h2>
            <TaskForm
                onSubmit={handleSubmit}
                projectId={projectId}
                initialValues={editingTask || {}}
                onCancel={() => setEditingTask(null)}
            />

            <ul>
                {tasks.map((task) => (
                    <li
                        key={task.id}
                        style={{
                            textDecoration: task.isCompleted ? "line-through" : "none",
                            display: "flex",
                            alignItems: "center",
                            gap: "10px",
                        }}
                    >
                        <input
                            type="checkbox"
                            checked={task.isCompleted}
                            onChange={() => handleToggleComplete(task)}
                        />
                        <span>
                            {task.title} - {new Date(task.dueDate).toLocaleDateString()}
                        </span>
                        <button onClick={() => setEditingTask(task)}>✏️</button>
                        <button onClick={() => handleDelete(task.id)}>❌</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

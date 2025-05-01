import { useState } from "react";
import { register } from "../api/auth";

export default function RegisterPage() {
    const [form, setForm] = useState({
        username: "",    
        email: "",
        password: ""
    });

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await register(form);
            alert("Ro'yxatdan o'tish muvaffaqiyatli!");
        } catch (err) {
            if (err.response?.data) {
                console.error(err.response.data);
                alert("Xato: " + JSON.stringify(err.response.data));
            } else {
                alert("Ro'yxatdan o'tishda xatolik yuz berdi.");
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Register</h2>
            <input
                name="username"                
                placeholder="Username"
                value={form.username}
                onChange={handleChange}
            />
            <input
                name="email"
                placeholder="Email"
                value={form.email}
                onChange={handleChange}
            />
            <input
                name="password"
                type="password"
                placeholder="Password"
                value={form.password}
                onChange={handleChange}
            />
            <button type="submit">Ro'yxatdan o'tish</button>
        </form>
    );
}

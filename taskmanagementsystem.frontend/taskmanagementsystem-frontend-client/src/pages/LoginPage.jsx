import { useState, useContext } from "react";
import { login as loginUser } from "../api/auth";
import { AuthContext } from "../contexts/AuthContext";

export default function LoginPage() {
    const [form, setForm] = useState({
        username: "",    
        password: ""
    });
    const { login } = useContext(AuthContext);

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const data = await loginUser(form);
            login(data.token);
            alert("Tizimga muvaffaqiyatli kirdingiz!");
        } catch (err) {
            if (err.response?.data) {
                alert("Login xatosi: " + JSON.stringify(err.response.data));
            } else {
                alert("Kirishda noma’lum xato yuz berdi.");
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Login</h2>
            <input
                name="username"                    
                placeholder="Username"
                value={form.username}
                onChange={handleChange}
            />
            <input
                name="password"
                type="password"
                placeholder="Password"
                value={form.password}
                onChange={handleChange}
            />
            <button type="submit">Kirish</button>
        </form>
    );
}

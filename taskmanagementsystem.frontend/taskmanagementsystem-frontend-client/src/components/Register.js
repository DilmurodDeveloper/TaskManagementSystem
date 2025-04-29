import React, { useState } from 'react';

const Register = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const handleRegister = (e) => {
        e.preventDefault();
        if (password !== confirmPassword) {
            console.error('Parollar mos kelmayapti.');
            return;
        }
        console.log('Register', { email, password });
    };

    return (
        <div>
            <h2>Register</h2>
            <form onSubmit={handleRegister}>
                <input type="email" value={email} onChange={e => setEmail(e.target.value)} required />
                <input type="password" value={password} onChange={e => setPassword(e.target.value)} required />
                <input type="password" value={confirmPassword} onChange={e => setConfirmPassword(e.target.value)} required />
                <button type="submit">Register</button>
            </form>
        </div>
    );
};

export default Register;

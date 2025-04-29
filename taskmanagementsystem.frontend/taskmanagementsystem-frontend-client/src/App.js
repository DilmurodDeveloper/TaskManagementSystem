import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'; 

import Login from './components/Login'; 
import Register from './components/Register'; 

const App = () => {
    return (
        <Router>
            <div>
                <h1>Task Management System</h1>
                <Routes> 
                    <Route path="/login" element={<Login />} /> 
                    <Route path="/register" element={<Register />} /> 
                </Routes>
            </div>
        </Router>
    );
};

export default App;

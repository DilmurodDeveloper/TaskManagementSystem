import { Outlet, Link } from 'react-router-dom';

function Layout() {
    return (
        <div>
            <nav style={{ padding: '10px', background: '#eee' }}>
                <Link to="/" style={{ marginRight: '10px' }}>Home</Link>
                <Link to="/login" style={{ marginRight: '10px' }}>Login</Link>
                <Link to="/register">Register</Link>
            </nav>
            <main style={{ padding: '20px' }}>
                <Outlet />
            </main>
        </div>
    );
}

export default Layout;

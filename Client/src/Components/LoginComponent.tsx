import {Api} from "../Models/Api.ts";
import {useState} from "react";

const api = new Api();

function LoginComponent(){
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");


    const handleLogin = async () => {
        try {
            const response = await api.user.login({ username, password });

            if (response.data.success) {
                alert('Login successful');
            } else {
                setError('Invalid username or password');
            }
        } catch (error) {
            setError('Error logging in');
            console.error(error);
        }
    };

    return (
        <>
            <h2>Welcome to Login</h2>
            <div>
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
            </div>
            <div>
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
            </div>
            <button onClick={handleLogin}>Login</button>
            {error && <p style={{ color: 'red' }}>{error}</p>}
        </>
    );
}

export default LoginComponent;
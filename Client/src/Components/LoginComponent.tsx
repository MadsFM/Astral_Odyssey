import { Api } from "../Models/Api.ts";
import { useState } from "react";

const api = new Api();

function LoginComponent() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async () => {
        try {
            const res = await api.user.loginCreate({username, password});
            if (res.data && res.data.token) {
                localStorage.setItem("authToken", res.data.token);
                const token = localStorage.getItem("authToken");
                const headers = {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'application/json'
                };

                alert("Login successful");
            } else {
                setError("Invalid username or password");
            }
        } catch (error) {
            console.error("Login failed:", error);
            setError("An error occurred during login process, please try again");
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

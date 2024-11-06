import { Api } from "./imports.ts";
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
        <div className="flex flex-col items-center justify-center h-screen bg-gray-900 text-yellow-400">
            <h2 className="text-3xl mb-4 font-bold">Welcome to Login</h2>
            <div className="w-80 space-y-4">
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    className="input input-bordered w-full bg-gray-800 text-yellow-400 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-yellow-500"
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="input input-bordered w-full bg-gray-800 text-yellow-400 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-yellow-500"
                />
                <button
                    onClick={handleLogin}
                    className="btn btn-primary w-full bg-blue-600 text-yellow-400 hover:bg-green-500 hover:text-gray-900 transition-colors duration-300 font-bold"
                >
                    Login
                </button>
                {error && <p className="text-red-500 mt-2">{error}</p>}
            </div>
        </div>
    );
}

export default LoginComponent;

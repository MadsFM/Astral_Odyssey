import {Api, ROUTES} from "./imports.ts";
import { useState } from "react";
import {useNavigate} from "react-router-dom";

const api = new Api({
    baseURL: "http://localhost:9000",
});

function LoginComponent() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();



    const handleLogin = async () => {
        try {
            const res = await api.user.loginCreate({username, password});
            if (res.data && res.data.token && res.data.roles) {
                localStorage.setItem("authToken", res.data.token);

                if (res.data.roles === "Admin")
                alert("Login successful");
                navigate(ROUTES.MAIN)
            } else {
                setError("Invalid username or password");
            }
        } catch (error) {
            //@ts-ignore
            if (error.response && error.response.status === 401) {
                setError("Invalid username or password");
            } else {
                console.error("Login failed:", error);
                setError("An error occurred, please try again");
                setTimeout(() => {
                    //@ts-ignore
                    setError(null);
                }, 2000);
            }
        }
    };

    const handleRegisterClicked = async () => {
        navigate(ROUTES.REGISTER)
    }

    return (
        <div
            className="relative flex flex-col items-center justify-center h-screen bg-cover bg-center text-yellow-400"
            style={{
                backgroundImage: `url("/pictures/login.webp")`,
                backgroundSize: "cover",
                backgroundPosition: "center",
            }}
        >
            <div className="absolute inset-0 bg-black opacity-50"></div>

            <div className="relative z-10">
                <h2 className="text-3xl mb-14 font-bold">Welcome traveler</h2>
                <div className="w-80 space-y-4">
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        className="input input-bordered w-full bg-gray-800 text-purple-500 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-purple-500"
                    />
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="input input-bordered w-full bg-gray-800 text-purple-500 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-purple-500"
                    />
                    <button
                        onClick={handleLogin}
                        className="btn btn-ghost w-full text-yellow-400 hover:bg-blue-500 hover:text-gray-900 transition-colors duration-300 font-bold border-blue-400"
                    >
                        Login
                    </button>
                    {error && <p className="text-red-500 mt-2">{error}</p>}
                    <button
                        onClick={handleRegisterClicked}
                        className="btn btn-ghost w-full text-yellow-400 hover:bg-green-500 hover:text-gray-900 transition-colors duration-300 font-bold border-green-400"
                    >
                        Create traveler
                    </button>
                </div>
            </div>
        </div>
    );
}

export default LoginComponent;

import {Api, ROUTES} from "../imports";
import {useState} from "react";
import {useNavigate} from "react-router-dom";

const api = new Api({
    baseURL: "http://localhost:9000",
});


function RegisterComponent() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [email, setEmail] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();


    const registerNewPlayer = async () => {
        if (password.length < 6) {
            setError("Password must be at least 6 characters long");
            return;
        }

        setLoading(true);
        setError(null);
        try {
            const userData = {
                username,
                email,
                passwordhash: password,
            };

            const res = await api.user.registerCreate(userData);
            alert("You registered, thank you")
            console.log("User registered successfully:", res.data);
            navigate(ROUTES.LOGIN);
        } catch (error){
            console.error("Error registering user:", error);
            //@ts-ignore
            setError("Registration failed, Please try again");
            setTimeout(() => {
                setError(null);
            }, 2000);
        }
    }


    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gray-900 text-yellow-400"
             style={{
                 backgroundImage: `url("/pictures/registration.webp")`,
                 backgroundSize: "cover",
                 backgroundPosition: "center",
             }}>

            <div className="absolute inset-0 bg-black opacity-50"></div>

            <div className="relative z-10">
                <h2 className="text-3xl mb-14 font-bold">Register as a Traveler</h2>
                <div className="w-80 space-y-4">
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        className="input input-bordered w-full bg-gray-800 text-purple-500 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-purple-500"
                    />
                    <input
                        type="email"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
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
                        onClick={registerNewPlayer}
                        className="btn btn-ghost w-full text-yellow-400 hover:bg-green-500 hover:text-gray-900 transition-colors duration-300 font-bold border-green-400"
                    >
                        Register traveler
                    </button>
                    {error && <p className="text-red-500 mt-2">{error}</p>}
                    <button
                        onClick={() => navigate(ROUTES.LOGIN)}
                        className="btn btn-ghost w-full text-yellow-400 hover:bg-red-500 hover:text-gray-900 transition-colors duration-300 font-bold border-red-400">
                        Back
                    </button>
                </div>
            </div>
        </div>
            );
            }

            export default RegisterComponent;
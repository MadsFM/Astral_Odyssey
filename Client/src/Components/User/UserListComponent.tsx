import {useEffect, useState} from "react";
import {Api, User} from "../../Models";
import axios from "axios";
import {FaPen, FaTrash, FaEnvelopeSquare, FaUserPlus} from "react-icons/fa";
import {FaSquareEnvelope} from "react-icons/fa6";

//@ts-ignore
const api = new Api();

function UserListComponent(){
    const [users, setUsers] = useState<User[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState("");

    const fetchUsers = async () => {
        try {
            const token = localStorage.getItem("authToken");
            const res = await axios.get("http://localhost:9000/User/getAll", {
                headers: { Authorization: `Bearer ${token}` }
            });
            console.log("API response:", res.data);
            setUsers(res.data || []);
        } catch (error) {
            console.error("Failed to load users", error);
            setError("Failed to load users");
        }
        setLoading(false);
    };

    const handleAddUser = () => {
        console.log("Add new user");
        // Implement the add user functionality here
    };

    const handleUpdate = (userId: string) => {
        console.log("Update user:", userId);
        // Implement update logic here
    };

    const handleSendMail = (userId: string) => {
        console.log("Send mail to:", userId);
        // Implement mail sending functionality here
    };

    const handleDelete = (userId: string) => {
        console.log("Delete user:", userId);
        // Implement delete logic here
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    if (loading) {
        return <div className="text-center text-white">Loading...</div>;
    }

    if (error) {
        return <div className="text-center text-red-500">{error}</div>;
    }

    return (
        <div className="container mx-auto p-6">
            <div className="flex justify-between items-center mb-6">
                <h2 className="text-3xl font-bold text-yellow-500">Users</h2>
                <button
                    className="flex items-center bg-gray-900 text-yellow-500 px-4 py-2 rounded-lg shadow hover:bg-yellow-500 hover:text-gray-900 transition-colors duration-200"
                >
                    <FaUserPlus className="mr-2"/> Add User
                </button>
            </div>
            <table className="min-w-full bg-gray-800 shadow-md rounded-lg text-left text-white">
                <thead>
                <tr>
                    <th className="py-3 px-4 border-b border-gray-700 bg-gray-900 text-yellow-400">Username</th>
                    <th className="py-3 px-4 border-b border-gray-700 bg-gray-900 text-yellow-400">Email</th>
                    <th className="py-3 px-4 border-b border-gray-700 bg-gray-900 text-yellow-400">Actions</th>
                </tr>
                </thead>
                <tbody>
                {users.map(user => (
                    <tr key={user.userid} className="hover:bg-gray-700 transition-colors duration-200">
                        <td className="py-3 px-4 border-b border-gray-700">{user.username}</td>
                        <td className="py-3 px-4 border-b border-gray-700">{user.email}</td>
                        <td className="py-3 px-4 border-b border-gray-700 flex space-x-3">
                            <button className="text-blue-400 hover:text-blue-500">
                                <FaPen />
                            </button>
                            <button className="text-blue-400 hover:text-blue-500">
                                <FaSquareEnvelope />
                            </button>
                            <button className="text-red-400 hover:text-red-500">
                                <FaTrash />
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}

export default UserListComponent;

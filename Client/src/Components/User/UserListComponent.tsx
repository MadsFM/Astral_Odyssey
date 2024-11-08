import {useEffect, useState} from "react";
import {Api, UserDto} from "../../Models";
import axios from "axios";

const api = new Api({
    baseURL: "http://localhost:9000",
});

function UserListComponent(){
    const [users, setUsers] = useState<UserDto[]>([]);
    const [loading, setLoading] = useState<boolean>(true)
    const [error, setError] = useState("");



    const fetchUsers = async () => {
        try {
            const token = localStorage.getItem("authToken"); // Ensure token is retrieved correctly
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

    useEffect(() => {
        fetchUsers();
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return(
        <div className="container mx-auto">
            <h2 className="text-2xl font-bold mb-4">User List</h2>
            <table className="min-w-full bg-white">
                <thead>
                <tr>
                    <th className="py-2 px-4 border-b">Username</th>
                    <th className="py-2 px-4 border-b">Email</th>
                </tr>
                </thead>
                <tbody>
                {users.map(user => (
                    <tr key={user.userid}>
                        <td className="py-2 px-4 border-b">{user.username}</td>
                        <td className="py-2 px-4 border-b">{user.email}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}

export default UserListComponent;
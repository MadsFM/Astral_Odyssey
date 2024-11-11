import { FaBell, FaUserCircle, FaSearch, FaCog, FaSignOutAlt } from 'react-icons/fa';

const TopBar = ({ username }) => {

    return (
        <div className="w-full h-16 bg-gray-800 flex items-center justify-between px-6">
            {/* Left Side - Search Bar */}
            <div className="flex items-center space-x-4">
                <div className="relative">
                    <input
                        type="text"
                        placeholder="Search..."
                        className="input input-bordered w-64"
                    />
                    <FaSearch className="absolute top-2 right-2 text-gray-400" />
                </div>
            </div>


            {/* Center - Game Title */}
            <h1 className="text-xl text-white font-semibold">Astral Odyssey</h1>

            {/* Right Side - User Info and Icons */}
            <div className="flex items-center space-x-6">
                <FaBell className="text-white text-xl cursor-pointer" />
                <div className="text-white flex items-center space-x-2">
                    <FaUserCircle className="text-2xl" />
                    <span>{username}</span>
                </div>
                <FaCog className="text-white text-xl cursor-pointer" />
                <FaSignOutAlt className="text-white text-xl cursor-pointer" />
            </div>
        </div>
    );
};

export default TopBar;

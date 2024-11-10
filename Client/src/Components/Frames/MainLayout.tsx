import { useState } from 'react';
import UserListComponent from '../User/UserListComponent'; // Import your UserListComponent
import TopBar from '../Frames/TopBar'; // Import the TopBar component
import Sidebar from './SideBar.tsx'; // Import the Sidebar component

const MainLayout = () => {
    const [currentTab, setCurrentTab] = useState('users');

    const renderContent = () => {
        switch (currentTab) {
            case 'users':
                return <UserListComponent />;
            case 'quests':
                return <div>Quests Component</div>;
            case 'universes':
                return <div>Universes Component</div>;
            case 'planets':
                return <div>Planets Component</div>;
            case 'roles':
                return <div>Roles Component</div>;
            default:
                return <div>Select a tab</div>;
        }
    };

    return (
        <div className="h-screen flex flex-col bg-gray-900">
            <TopBar username="Mads" />

            <div className="flex flex-1">
                <Sidebar setCurrentTab={setCurrentTab} currentTab={currentTab} />

                <div className="flex flex-1 items-center justify-center">
                    <div className="bg-gray-800 p-6 w-3/4 rounded-lg shadow-md">
                        {renderContent()}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default MainLayout;

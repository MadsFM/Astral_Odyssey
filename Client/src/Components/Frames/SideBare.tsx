const Sidebar = ({ setCurrentTab, currentTab }) => {
    const tabs = [
        { label: 'Users', key: 'users' },
        { label: 'Quests', key: 'quests' },
        { label: 'Universes', key: 'universes' },
        { label: 'Planets', key: 'planets' },
        { label: 'Roles', key: 'roles' },
    ];

    return (
        <div className="w-1/4 h-screen bg-gray-800 p-4 flex flex-col items-start">
            {tabs.map((tab) => (
                <button
                    key={tab.key}
                    onClick={() => setCurrentTab(tab.key)}
                    className={`w-full my-2 text-left py-2 px-4 rounded-lg transition-colors ${
                        currentTab === tab.key
                            ? 'bg-gray-700 text-white font-bold'
                            : 'text-gray-400 hover:bg-gray-700 hover:text-white'
                    }`}
                >
                    {tab.label}
                </button>
            ))}
        </div>
    );
};

export default Sidebar;
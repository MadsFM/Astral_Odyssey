

//@ts-ignore
const Sidebar = ({ setCurrentTab, currentTab }) => {
    const tabs = [
        { label: 'Users', key: 'users' },
        { label: 'Quests', key: 'quests' },
        { label: 'Universes', key: 'universes' },
        { label: 'Planets', key: 'planets' },
        { label: 'Roles', key: 'roles' },
    ];

    return (
        <div className="w-1/4 h-screen bg-gray-900 p-4 flex flex-col items-start border-r border-gray-700 shadow-lg">
            {tabs.map((tab) => (
                <button
                    key={tab.key}
                    onClick={() => setCurrentTab(tab.key)}
                    className={`w-full my-2 text-left py-2 px-4 rounded-lg font-bold tracking-wide text-gray-300 transition duration-300 ${
                        currentTab === tab.key
                            ? 'bg-green-400 text-black shadow-lg'
                            : 'hover:text-black hover:bg-gray-800'
                    } ${
                        currentTab !== tab.key
                            ? 'hover:shadow-[0_0_15px_5px_rgba(34,197,94,0.5)]'
                            : ''
                    }`}
                >

          <span
              className={`relative inline-block ${
                  currentTab === tab.key ? 'text-black' : 'text-gray-300'
              }`}
          >
            {tab.label}
              <span
                  className="absolute inset-0 transform scale-x-0 transition-transform duration-300 ease-out
              bg-yellow-400 opacity-30 rounded-lg
              group-hover:scale-x-100"
              />
          </span>
                </button>
            ))}
        </div>
    );
};

export default Sidebar;
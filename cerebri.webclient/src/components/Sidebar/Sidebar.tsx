import { ReactNode, useState } from "react";
import { MdMenuOpen } from "react-icons/md";
import { Home } from "..";

interface SidebarProps {
    setActiveComponent: (component: ReactNode) => void;
}

const Sidebar: React.FC<SidebarProps> = ({ setActiveComponent }) => {
    const [isOpen, setIsOpen] = useState<boolean>(true);
    return (
        <div
            className={`bg-slate-400 text-white h-full transition-all duration-300 items-center flex flex-col ${
                isOpen ? "w-64" : "w-10"
            }`}
        >
            <div 
                className="flex flex-row w-full mr-4 justify-end cursor-pointer"
                onClick={() => setIsOpen(!isOpen)}
            >
                <MdMenuOpen size={24}/>
            </div>

            <div className={`flex flex-col text-lg text-center transition-all duration-100 ${isOpen ? 'visible' : 'invisible' }`}>
                <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<Home />)}>Home</a>
                <a className="cursor-pointer hover:opacity-50">Journals</a>
                <a className="cursor-pointer hover:opacity-50">Mood Reports</a>

            </div>
        </div>
    );
};

export default Sidebar;
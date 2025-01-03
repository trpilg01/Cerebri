import { ReactNode, useState } from "react";
import { MdMenuOpen } from "react-icons/md";
import { IoSettingsOutline } from "react-icons/io5";
import { CiLogout } from "react-icons/ci";
import { CheckIns, Home, Journals } from "..";

interface SidebarProps {
    setActiveComponent: (component: ReactNode) => void;
}

const Sidebar: React.FC<SidebarProps> = ({ setActiveComponent }) => {
    const [isOpen, setIsOpen] = useState<boolean>(true);
    return (
        <div
            className={`bg-slate-400 text-white h-screen transition-all duration-300 items-center flex flex-col ${
                isOpen ? "w-64" : "w-10"
            }`}
        >
            <div 
                className="flex flex-row w-full mr-4 justify-end cursor-pointer"
                onClick={() => setIsOpen(!isOpen)}
            >
                <MdMenuOpen size={24}/>
            </div>

            <div className={`flex flex-col h-full w-full justify-between text-lg text-center transition-all duration-100 ${isOpen ? 'visible' : 'invisible' }`}>
                <div className="flex flex-col text-lg text-center">
                    <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<Home />)}>Home</a>
                    <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<Journals />)}>Journals</a>
                    <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<CheckIns />)}>Check-Ins</a>
                    <a className="cursor-pointer hover:opacity-50">Mood Reports</a>
                </div>

                <div 
                    className="flex flex-col bg-slate-200 h-fit w-full"
                >
                    <div className="flex flex-row p-2 items-center cursor-pointer gap-2">
                        <CiLogout size={24} color="black"/>
                        <a className="text-black">Logout</a>
                    </div>
                    <div className="flex flex-row p-2 items-center cursor-pointer gap-2">
                        <IoSettingsOutline size={24} color="black"/>
                        <a className="text-black">Settings</a>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Sidebar;
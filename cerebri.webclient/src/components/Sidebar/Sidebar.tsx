import { ReactNode, useState } from "react";
import { MdMenuOpen } from "react-icons/md";
import { IoSettingsOutline } from "react-icons/io5";
import { CiLogout } from "react-icons/ci";
import { useNavigate } from "react-router-dom";
import { CheckIns, Home, Journals, Reports, ProfileInfo } from "..";

interface SidebarProps {
    setActiveComponent: (component: ReactNode) => void;
}

const Sidebar = ( props: SidebarProps ) => {
    const { setActiveComponent } = props;
    const [isOpen, setIsOpen] = useState<boolean>(true);
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate('/');
    }

    return (
        <div
            className={`bg-bittersweet text-white h-screen transition-all duration-300 items-center flex flex-col ${
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
                <div className="flex flex-col text-lg text-center font-semibold">
                    <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<Home />)}>Home</a>
                    <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<Journals />)}>Journals</a>
                    <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<CheckIns />)}>Check-Ins</a>
                    <a className="cursor-pointer hover:opacity-50" onClick={() => setActiveComponent(<Reports />)}>Reports</a>
                </div>

                <div className="flex flex-col h-fit w-full">
                    <div 
                        className="flex flex-row p-2 items-center cursor-pointer gap-2"
                        onClick={handleLogout}
                    >
                        <CiLogout size={24} />
                        <a className="font-semibold">Logout</a>
                    </div>
                    <div className="flex flex-row p-2 items-center cursor-pointer gap-2" onClick={() => setActiveComponent(<ProfileInfo />)}>
                        <IoSettingsOutline size={24} />
                        <a className="font-semibold">Profile</a>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Sidebar;
import { useState } from "react";
import { MdMenuOpen } from "react-icons/md";
import { IoSettingsOutline } from "react-icons/io5";
import { CiLogout } from "react-icons/ci";
import { useNavigate, Link } from "react-router-dom";

const Sidebar = () => {
    const [isOpen, setIsOpen] = useState<boolean>(true);
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate('/');
    };

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
                    <Link to='/content/'>Home</Link>
                    <Link to='/content/journals'>Journals</Link>
                    <Link to='/content/checkins'>Check-Ins</Link>
                    <Link to='/content/reports'>Reports</Link>
                </div>

                <div className="flex flex-col h-fit w-full items-center">
                    <div 
                        className="flex flex-row p-2 items-center cursor-pointer gap-4"
                        onClick={handleLogout}
                    >
                        <CiLogout size={24} />
                        <a className="font-semibold" onClick={handleLogout}>Logout</a>
                    </div>
                    <div className="flex flex-row p-2 items-center cursor-pointer gap-4">
                        <IoSettingsOutline size={24} />
                        <Link to='/content/settings' className="font-semi">Settings</Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Sidebar;
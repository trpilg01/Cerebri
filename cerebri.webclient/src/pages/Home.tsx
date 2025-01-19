import { ReactNode, useEffect, useState } from "react";
import { CreateCheckIn, CreateJournal } from "components";
import { requestUserInfo } from "services";

const Home = () => {
    const [activeOverlay, setActiveOverlay] = useState<ReactNode | null>(null);
    const [showOverlay, setShowOverlay] = useState<boolean>(false);
    const [firstName, setFirstName] = useState<string | undefined>(undefined);

    const handleActiveOverlayChange = (overlay: ReactNode) => {
        setActiveOverlay(overlay);
        setShowOverlay(true);
    };

    const renderActiveOverlay = () => {
        return activeOverlay;
    };

    useEffect(() => {
        const fetchUserInfo = async () => {
            const userInfo = await requestUserInfo();
            setFirstName(userInfo?.firstName);
        }
        fetchUserInfo()
    }, []);

    return (
        <div 
            className="h-full w-full flex flex-col p-10 bg-pearlLusta justify-center items-center content-center text-center"
        >
            <h1 className="mb-10 text-3xl font-bold">{firstName ? `Welcome, ${firstName}` : "Welcome"}</h1>
            <div className="flex flex-row w-2/3 justify-evenly">
                
                <div 
                    className="h-40 w-40 bg-blizzardBlue text-center cursor-pointer content-center rounded-full shadow-xl transition-transform 
                               transform hover:-translate-y-2"
                    onClick={() => handleActiveOverlayChange(<CreateCheckIn setShowOverlay={setShowOverlay} />)}
                >
                    <a className="font-bold text-xl">Check In</a>
                </div>
                
                <div className="h-40 w-40 bg-seaPink text-center cursor-pointer content-center rounded-full shadow-xl transition-transform 
                                transform hover:-translate-y-2"
                     onClick={() => handleActiveOverlayChange(<CreateJournal setShowOverlay={setShowOverlay}/>)}
                >
                    <a className="font-bold text-xl">Write</a>
                </div>
                
            </div>
            {showOverlay && renderActiveOverlay()}
        </div>
    );
};

export default Home;
import { ReactNode, useState } from "react";
import { CheckIn, WriteComponent } from "..";

const Home: React.FC = () => {
    const [activeOverlay, setActiveOverlay] = useState<ReactNode | null>(null);
    const [showOverlay, setShowOverlay] = useState<boolean>(false);
    const [firstName, setFirstName] = useState<string | null>(null);

    const handleActiveOverlayChange = (overlay: ReactNode) => {
        setActiveOverlay(overlay);
        setShowOverlay(true);
    };

    const renderActiveOverlay = () => {
        return activeOverlay;
    };

    return (
        <div 
            className="h-full w-full flex flex-col p-10 bg-pearlLusta justify-center items-center content-center text-center"
        >
            <h1 className="mb-10 text-3xl font-bold">{firstName ? `Welcome, ${firstName}` : "Welcome"}</h1>
            <div className="flex flex-row w-2/3 justify-evenly">
                
                <div className="h-40 w-40 bg-seaPink text-center cursor-pointer content-center rounded-full shadow-xl transition-transform 
                                transform hover:-translate-y-2"
                     onClick={() => handleActiveOverlayChange(<CheckIn setShowOverlay={setShowOverlay}/>)}
                >
                    <a className="font-bold text-xl">Check In</a>
                </div>
                
                <div className="h-40 w-40 bg-bittersweet text-center cursor-pointer content-center rounded-full shadow-xl transition-transform 
                                transform hover:-translate-y-2"
                     onClick={() => handleActiveOverlayChange(<WriteComponent setShowOverlay={setShowOverlay}/>)}
                >
                    <a className="font-bold text-xl">Write</a>
                </div>
            </div>
            {showOverlay && renderActiveOverlay()}
        </div>
    );
};

export default Home;
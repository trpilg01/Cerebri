import { Header } from 'components';
import { useState } from 'react';
import { Login } from 'components';
import './LandingPage.css';


const LandingPage = () => {
    const [isShowLoginOverlay, setIsShowLoginOverlay] = useState<boolean>(false);

    return (
        <div className="flex-container column">
            <Header setShowLogin={setIsShowLoginOverlay} />

            {isShowLoginOverlay && <Login setShowLogin={setIsShowLoginOverlay} />}
        </div>
    );
};

export default LandingPage;
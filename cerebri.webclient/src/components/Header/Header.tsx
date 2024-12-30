import './Header.css';

interface HeaderProps {
    setShowLogin: (value: boolean) => void;
}

const Header: React.FC<HeaderProps> = ({ setShowLogin }) => {
    return (
        <div className="header-container">
            <h1 className="font-bold text-xl" >Cerebri</h1>
            <div className="header-container__links">
                <a>About</a>
                <a>Contact</a>
                <a onClick={() => setShowLogin(true)}>Login</a>
            </div>
        </div>
    );
};

export default Header;
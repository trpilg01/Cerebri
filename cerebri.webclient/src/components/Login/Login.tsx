import React, { useState } from 'react';
import { IoMdClose } from "react-icons/io";
import { useNavigate } from 'react-router-dom';
import './Login.css';
import { LoginRequest } from 'data/dataTypes';
import { requestLogin } from 'services';

interface LoginProps {
    setShowLogin: (value: boolean) => void;
}

const Login: React.FC<LoginProps> = ({ setShowLogin }) => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (email.length === 0 || password.length === 0) {
            setError("Please fill out all fields");
            return;
        }

        try {
            const request: LoginRequest = { email: email, password: password }
            const response = await requestLogin(request);

            if (response === true) {
                navigate('/content');
            }
            if (typeof(response) === 'string') {
                setError(response);
            }
        } catch (error: any) {
            setError(error.message);
            return;
        }
    }
    
    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <div className="login-overlay">
                <div className="login-overlay__detail">
                    <h1>Cerebri</h1>
                </div>

                <div className="flex flex-col justify-center items-center w-1/3">
                    <div className='fixed top-5 right-8 cursor-pointer' onClick={() => setShowLogin(false)}>
                        <IoMdClose size={20} />
                    </div>
                    <form 
                        onSubmit={(e) => handleSubmit(e)} 
                        className='bg-gray-100 p-6 rounded-lg shadow-md w-80 h-1/3 flex flex-col justify-center items-center space-y-5'
                    >
                        <h3 className='text-center text-lg font-extrabold'>Cerebri</h3>
                        <div className='flex flex-row items-center'>
                            <input
                                type='text'
                                id='email'
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                className='p-2 mb-4 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500'
                                placeholder="email@email.com"
                            />
                        </div>

                        <div className='flex flex-row items-center align'>
                            <input
                                type='password'
                                id='password'
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                className='p-2 mb-4 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-500'
                                placeholder="Password"
                            />
                        </div>

                        <button
                            type="submit" 
                            className='bg-blue-500 rounded w-20 cursor-pointer font-bold' 
                        >
                            Login
                        </button>
                    </form>
                    {error && (
                            <div className="mt-3 h-fit w-fit text-center text-red-500">
                                <h1>Error Logging in:</h1>
                                <a>{error}</a>
                            </div>
                        )}
                </div>
            </div>
        </div>
    );
};

export default Login;
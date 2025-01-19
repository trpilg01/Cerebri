import cerebriLogo  from 'assets/cerebri_logo.png';
import { LoginRequest } from 'data/dataTypes';
import { FormEvent, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { requestLogin } from 'services';

const LoginPage = () => {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(null);
        if (email.length === 0 || password.length === 0) {
            setError("All fields are required");
        }

        const request: LoginRequest = { email: email, password: password }
        
        try {
            const response = await requestLogin(request);
            if (response.status === 200){
                navigate('/content/');
            }
        } catch (err: any) {
            if (err.response && err.response.status === 400){
                setError("Invalid email or password");
            } else {
                setError("An unexpected error occurred. Please try again later");
            }
        }
    }

    return (
        <div className="flex h-screen w-full bg-pearlLusta overflow-hidden">
            <div className='flex flex-row items-center w-full h-full'>
                {/* Logo */}
                <div className='flex w-3/4 h-full justify-center items-center'>
                    <div className='flex h-3/4 w-3/4 justify-center items-center'>
                        <img 
                            src={cerebriLogo}
                            alt='Image'
                            className='h-full rounded-full shadow-2xl'
                        />
                    </div>
                </div>
                {/* Login */}
                <div className='flex flex-col w-1/4 h-full bg-bittersweet shadow-inner p-2 justify-center items-center'>
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
                    {error && <a className='mt-2 font-bold'>{error}</a>}
                </div>
            </div>
        </div>
    );
};

export default LoginPage;
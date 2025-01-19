import { Sidebar, Journals, CheckIns, Reports } from 'components';
import { Routes, Route } from 'react-router-dom';
import ErrorPage from './ErrorPage';
import { Home, Settings } from '.';

const ContentPage = () => {
    return (
        <div className='flex h-screen'>
            <Sidebar />

            <div className='flex-1'>
                <Routes>
                    <Route path='/' element={<Home />} />
                    <Route path='journals' element={<Journals />} />
                    <Route path='checkins' element={<CheckIns />} />
                    <Route path='reports' element={<Reports />} />
                    <Route path='settings' element={<Settings />}/>
                    <Route path='*' element={<ErrorPage />} />
                </Routes>
            </div>
        </div>
    );
};

export default ContentPage;
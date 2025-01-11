import { Home, Sidebar } from 'components';
import { ReactNode, useState } from 'react';

const ContentPage = () => {
    const [activeComponent, setActiveComponent] = useState<ReactNode>(<Home />)
    
    const renderActiveComponent = () => {
        return activeComponent;
    }
    
    return (
        <div className='flex h-screen'>
            <Sidebar setActiveComponent={setActiveComponent} />
            
            {renderActiveComponent()}
        </div>
    );
};

export default ContentPage;
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { LandingPage, ContentPage } from 'pages';
import { ProtectedRoute } from 'components';
function App() {

    return (
        <Router>
            <Routes>
                <Route path="/" element={<LandingPage />} />
                <Route
                    path="/content"
                    element={
                        <ProtectedRoute>
                            <ContentPage />
                        </ProtectedRoute>
                    }
                />
                <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
        </Router>
    );
};

export default App;
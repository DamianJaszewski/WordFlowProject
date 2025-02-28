import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import "@fontsource/poppins";
/*import 'bootstrap-icons/font/bootstrap-icons.css';*/

import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Navigation from "./components/Navigation";
import Account from './pages/Account';
import Learn from './pages/Learn'

function App() {

    return (
        <BrowserRouter>
            <Navigation />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
                <Route path="account" element={<Account />} />
                <Route path="learn" element={<Learn />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
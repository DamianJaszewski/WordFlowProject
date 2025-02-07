
import { ContainerWrapper, CustomButton } from "../components";
import { userService } from "../services/userService";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from 'react';

function Account() {

    const navigate = useNavigate(); // Hook do nawigacji
    const [user, setUser] = useState([]);

    useEffect(() => {
        handleUserData();
    }, []);

    const handleUserData = async () => {
        const response = await userService.getUser();
        setUser(response);
        if (response === null) {
            navigate("/login");
        }
    }

    const handleLogout = async () => {
        const response = await userService.logout();
        console.log(response);
        if (response === null) {
            navigate("/login");
        }
    }

    return (
        <ContainerWrapper maxWidth="400px" heading="Konto">
            <p>{user.userName}</p>
            <CustomButton title="Wyloguj" onClick={handleLogout} />
        </ContainerWrapper>
    );
}

export default Account;
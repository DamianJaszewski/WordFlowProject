
import { useState } from 'react';

import { Form } from "react-bootstrap";
import { ContainerWrapper, InputWrapper, CustomButton } from "../components";
import { userService } from "../services/userService";
import { useNavigate } from "react-router-dom";

function Register() {

    const initialNewUser = {
        userName: '',
        email: '',
        phoneNumber: '',
        password: ''
    }

    const [newUser, setNewUser] = useState(initialNewUser);
    const navigate = useNavigate(); // Hook do nawigacji

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await userService.register(newUser);
            navigate("/");
        } catch (error) {
            console.error("Error during login:", error);
        }
    }

    return (
        <ContainerWrapper maxWidth="400px" heading="Rejestracja">
            <Form onSubmit={handleSubmit}>
                <InputWrapper
                    controlId="formName"
                    type="name"
                    placeholder="Nazwa"
                    value={newUser.userName}
                    onChange={(e) => setNewUser({ ...newUser, userName: e.target.value })}
                />
                <InputWrapper
                    controlId="formEmail"
                    typeName="email"
                    placeholder="Email"
                    iconName="bi bi-envelope"
                    value={newUser.email}
                    onChange={(e) => setNewUser({ ...newUser, email: e.target.value })}
                />
                <InputWrapper
                    controlId="formPassword"
                    typeName="password"
                    placeholder="Hasło"
                    iconName="bi bi-key"
                    value={newUser.password}
                    onChange={(e) => setNewUser({ ...newUser, password: e.target.value })}
                />
                <CustomButton title="Załóż konto" />
            </Form>
        </ContainerWrapper>
    );
}

export default Register
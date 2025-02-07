
import { useState } from 'react';

import { Form } from "react-bootstrap";
import { ContainerWrapper, InputWrapper, CustomButton } from "../components";
import { userService } from "../services/userService";
import { Link } from 'react-router-dom'
import { useNavigate } from "react-router-dom";

function Login() {

    const initialUser = {
        email: '',
        password: ''
    }

    const [user, setUser] = useState(initialUser);
    const navigate = useNavigate(); // Hook do nawigacji

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await userService.login(user);
            navigate("/");
        } catch (error) {
            console.error("Error during login:", error);
        }
    }

    return (
        <ContainerWrapper maxWidth = "400px" heading = "Logowanie">
            <Form onSubmit={handleSubmit}>
                <InputWrapper
                    controlId="formEmail"
                    typeName="email"
                    placeholder="Email"
                    iconName="bi bi-envelope"
                    value={user.email}
                    onChange={(e) => setUser({ ...user, email: e.target.value })}
                />
                <InputWrapper
                    controlId="formPassword"
                    typeName="password"
                    placeholder="Hasło"
                    iconName="bi bi-key"
                    value={user.password}
                    onChange={(e) => setUser({ ...user, password: e.target.value })}
                />
                <div style={{ display: "flex",  justifyContent: "space-between"}}>
                    <Link class="btn" style={{
                        backgroundColor: "transparent" ,
                        color: "rgb(0 0 0)",
                        borderRadius: '4px',
                        boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.2)',
                        borderColor: "transparent",
                        marginTop: "1rem",
                        marginBottom: "1rem",
                        width: "45%"
                    }} to='/register'>Zarejestruj</Link>
                    <CustomButton title="Zaloguj" size="45%"/>
                </div>
            </Form>
        </ContainerWrapper>
    );
}

export default Login;
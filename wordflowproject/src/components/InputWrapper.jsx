
import { useState } from "react";
import { Form } from "react-bootstrap";

function InputWrapper ({ controlId, typeName, placeholder, value, onChange, className, iconName }) {

    const [showPassword, setShowPassword] = useState(false);

    const togglePasswordVisibility = () => {
        setShowPassword((prev) => !prev);
    };

    const isPasswordField = typeName === "password";

    return (
        <Form.Group controlId={controlId} className={className || "mb-3"} >
            <div style={{display: "flex"}}>
                <Form.Control
                    type={isPasswordField && showPassword ? "text" : typeName}
                    placeholder={placeholder}
                    value={value || ""}
                    onChange={onChange || (() => { })}
                    style={{
                        backgroundColor: "transparent",
                        borderColor: "transparent"
                    }}
                />
                {!isPasswordField && iconName && (
                    <i className={iconName} style={{ padding: ".375rem 1rem", fontSize: "1.2rem" }} />
                )}
                {isPasswordField && (
                    <i
                        className={showPassword ? "bi bi-eye" : "bi bi-eye-slash" }
                        onClick={togglePasswordVisibility}
                        style={{
                            padding: ".375rem 1rem",
                            fontSize: "1.2rem",
                            cursor: "pointer"
                        }}
                    />
                )}
            </div>
            <hr style={{ margin: "0.3rem", border: "1px solid" }} />
        </Form.Group>
    )
}

export default InputWrapper
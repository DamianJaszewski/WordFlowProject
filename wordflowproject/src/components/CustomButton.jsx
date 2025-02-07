
import { Button } from "react-bootstrap";

function CustomButton({ title, onClick, type = "submit", transparent = false, size = "100%" }) {

    return (
        <Button className="btn" style={{
            backgroundColor: transparent ? "transparent" : '#152837',
            color: transparent ? "rgb(0 0 0)" : '#fff',
            borderRadius: '4px',
            boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.2)',
            borderColor: "transparent",
            marginTop: "1rem",
            marginBottom: "1rem",
            width: size
        }} type={type} onClick={onClick || (() => { })}>
            {title}
        </Button>
    )
}

export default CustomButton
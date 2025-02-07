
import { Container } from "react-bootstrap";

function ContainerWrapper ({ maxWidth, children, heading }) {
    return (
        <Container className="mt-5" style=
            {{
                maxWidth: maxWidth,
                padding: "0.5rem",
                //background: "rgba(255, 255, 255, 0.2)", Półprzezroczyste tło
                backdropFilter: "blur(10px)", /* Efekt rozmycia */
                textShadow: "0px 1px 2px rgba(0, 0, 0, 0.15)"
            }}>
            <div style=
                {{
                    position: "relative",
                    zIndex: "1", /* Zapobiega dziedziczeniu efektu tła */
                    padding: "1.5rem",
                    borderRadius: "6px", /* Zaokrąglenie */
                    boxShadow: "0 4px 6px rgba(0, 0, 0, 0.1), 0 1px 3px rgba(0, 0, 0, 0.2)" /* Subtelny cień */
                }}>
            <h1 className="mb-3 mt-3">{heading}</h1>
                {children}
            </div>
        </Container>
    )
};

export default ContainerWrapper
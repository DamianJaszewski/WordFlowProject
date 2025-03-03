
import { useEffect, useState } from 'react';
import { Form, Modal } from "react-bootstrap";
import { ContainerWrapper, InputWrapper, CustomButton, CustomTable } from "../components";
import { cardService } from "../services/cardService";

function Home() {

    const initialCardState = { id: 0, labelId: 0, title: '', question: '', answer: '' };
    const [randomCard, setRandomCard] = useState();
    const [newTask, setNewTask] = useState(initialCardState);
    const [showModal, setShowModal] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [isVisible, setIsVisible] = useState(false);

    const handleModalToggle = () => setShowModal(!showModal);

    useEffect(() => {
        handleTaskData();
    }, []);

    const handleTaskData = async () => {
        const data = await cardService.getRandomCard();
        setRandomCard(data);
        console.log(data);
    }

    const handleNewTask = () => {
        setNewTask(initialCardState);
        setIsEditing(false);
        setShowModal(true);
    }

    const handleEditTask = (task) => {
        setNewTask(task);
        setIsEditing(true);
        setShowModal(true);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (isEditing) {
            console.log(newTask.id);
            await cardService.updateCard(newTask);
        } else {
            await cardService.createCard(newTask);
        }
        await handleTaskData(); // Odśwież listę zadań
        handleModalToggle();
    };

    const handleDeleteTask = async (task) => {
        await cardService.deleteTask(task.id)
        await handleTaskData();
    };

    const columns = [
        { header: "Tytuł", field: "title" },
        { header: "Szczegóły", field: "question" }
    ];

    const actions = [
        { method: handleEditTask, iconName: "bi bi-pencil" },
        { method: handleDeleteTask, iconName: "bi bi-trash" },
    ];

    return (
        <ContainerWrapper maxWidth="800px" heading="Nauka">
            <CustomButton title="Dodaj" onClick={handleNewTask} />
            <Modal show={showModal} onHide={handleModalToggle} centered>
                <Modal.Header closeButton>
                    <Modal.Title>Nowe Zadanie</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={handleSubmit}>
                        <div className="d-flex mb-3 gap-3" style={{flexWrap: "wrap"}}>
                        <InputWrapper
                            controlId="formTitle"
                            type="text"
                            placeholder="Tytuł"
                            value={newTask.title}
                            onChange={(e) => setNewTask({ ...newTask, title: e.target.value })}
                            className="flex-fill"
                        />
                        <InputWrapper
                            controlId="formDescription"
                            type="text"
                            placeholder="Pytanie"
                            value={newTask.question}
                            onChange={(e) => setNewTask({ ...newTask, question: e.target.value })}
                            className="flex-fill"
                        />
                        <InputWrapper
                            controlId="formDescription"
                            type="text"
                            placeholder="Odpowiedź"
                            value={newTask.answer}
                            onChange={(e) => setNewTask({ ...newTask, answer: e.target.value })}
                            className="flex-fill"
                        />
                        </div>
                        <CustomButton title="Dodaj" />
                    </Form>
                </Modal.Body>
            </Modal>
            {randomCard && (
                <div>
                    <h3>{randomCard.title}</h3>
                    <div>{randomCard.question}</div>
                </div>
            )}

            <div
                style={{ cursor: "pointer" }}
                className="relative w-64 h-32 bg-gray-200 border border-gray-300 rounded-xl overflow-hidden"
                onClick={() => setIsVisible(!isVisible)}
            >
                {!isVisible ? (
                    <div>
                        <span className="text-xl text-gray-700 font-semibold">Pokaż</span>
                    </div>
                ):
                    <div>
                        <p className="text-lg text-gray-900">{randomCard?.answer ?? "Brak odpowiedzi"}</p>
                    </div>
                }
            </div>
        </ContainerWrapper>
    )
}

export default Home
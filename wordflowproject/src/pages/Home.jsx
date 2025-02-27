
import { useEffect, useState } from 'react';
import { Form, Modal } from "react-bootstrap";
import { ContainerWrapper, InputWrapper, CustomButton, CustomTable } from "../components";
import { cardService } from "../services/cardService";

function Home() {

    const initialTaskState = { id: 0, labelId: 0, title: '', question: '' };
    const [myTasks, setMyTasks] = useState();
    const [newTask, setNewTask] = useState(initialTaskState);
    const [showModal, setShowModal] = useState(false);
    const [isEditing, setIsEditing] = useState(false);

    const handleModalToggle = () => setShowModal(!showModal);

    useEffect(() => {
        handleTaskData();
    }, []);

    const handleTaskData = async () => {
        const data = await cardService.getCards();
        setMyTasks(data);
    }

    const handleNewTask = () => {
        setNewTask(initialTaskState);
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
            await cardService.updateTask(newTask);
        } else {
            await cardService.createTask(newTask);
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
        <ContainerWrapper maxWidth="800px" heading="Zadania">
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
                            title="Tytuł"
                            type="text"
                            placeholder="Tytuł"
                            value={newTask.title}
                            onChange={(e) => setNewTask({ ...newTask, title: e.target.value })}
                            className="flex-fill"
                        />
                        <InputWrapper
                            controlId="formDescription"
                            title="Description"
                            type="text"
                            placeholder="Szczegóły"
                            value={newTask.description}
                            onChange={(e) => setNewTask({ ...newTask, description: e.target.value })}
                            className="flex-fill"
                        />
                        </div>
                        <CustomButton title="Dodaj" />
                    </Form>
                </Modal.Body>
            </Modal>
            <CustomTable
                columns={columns}
                data={myTasks}
                actions={actions}
            />
        </ContainerWrapper>
    )
}

export default Home
import axios from "axios";
import React, { ChangeEvent, useEffect, useState } from "react";
import { Button, Form, InputGroup, Col, Row } from "react-bootstrap";
import { Employee } from "../../models/Employee";
import { Modal } from "../Modal/Modal";

interface ModalProps {
    modalIsOpen: boolean;
    handleClose(): void;
    handleFormSubmit(): void;
    initialValues?: any
}
const customStyles: ReactModal.Styles = {
    content: {
        inset: '50% auto auto 50%',
        top: '50%',
        right: 'auto',
        bottom: 'auto',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: '100%',
        maxWidth: '540px'
    }
};

export const NewEmployeeModal = ({ modalIsOpen,
    handleClose,
    initialValues
}: ModalProps) => {
    const [positions, setPositions] = useState([])
    const [selectedOption, setSelectedOption] = useState(0);
    const [newEmp, setNewEmp] = useState<Employee>({ atelieID: (JSON.parse(localStorage.getItem('user')!).atelieID) as number } as Employee)

    const handleInfoChange = (property: string, e: string) => {
        setNewEmp(prev => ({ ...prev, [`${property}`]: e }))
    }

    useEffect(() => {
        const res = axios.get('http://localhost:3000/api/Positions').then(r => setPositions(r.data))
        console.log(positions)
    }, [])

    async function SubmitEmployee() {
        if (!initialValues)
            axios.post('http://localhost:3000/api/Employees', newEmp).then(r => r.data).then( r=> alert("Added new employee!"))
        else {
            let empl = { ...initialValues, ...newEmp }
            delete empl.position;
            delete empl.ordersCount;
            delete empl.ordersCost;
            delete empl.atelie;
            console.log(empl)
            axios.put(`http://localhost:3000/api/Employees/${empl.employeeID}`, empl).then(r => r.data).then( r=> alert("Updated employee information!"))
        }
        handleClose()
    }

    return (
        <Modal isOpen={modalIsOpen} handleCloseModal={handleClose} style={customStyles}>

            <Form.Group >
                <Form.Label>New employee personal information:</Form.Label>
                <Form>
                    <Row>
                        <Col>
                            <Form.Control defaultValue={initialValues && initialValues.firstname} placeholder='Firstname' onChange={(e) => handleInfoChange('firstname', e.target.value)} />
                        </Col>
                        <Col>
                            <Form.Control defaultValue={initialValues && initialValues.lastname} placeholder='Lastname' onChange={(e) => handleInfoChange('lastname', e.target.value)} />
                        </Col>
                    </Row>
                </Form>
            </Form.Group>
            <InputGroup size="sm" className="mb-2 center" >
                <InputGroup.Prepend>
                    <InputGroup.Text id="inputGroup-sizing-sm" aria-setsize={5}>Position</InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control as="select"
                    onChange={(e: any) => setNewEmp(prev => ({ ...prev, positionID: parseInt(e.target.value) }))} defaultValue={initialValues ? initialValues.positionID : 0}>
                    {positions && positions.map((p: { positionID: number, name: string }) => <option value={p.positionID}>{p.name}</option>)}
                </Form.Control>
            </InputGroup>
            <Form.Group>
                <Form.Label>Phone number:</Form.Label>
                <Form.Control defaultValue={initialValues && initialValues.phoneNumber} onChange={(e) => handleInfoChange('phoneNumber', e.target.value)} />
            </Form.Group>
            {newEmp.positionID !== 3 && <React.Fragment>
                <Form.Group>
                    <Form.Label>Email:</Form.Label>
                    <Form.Control defaultValue={initialValues && initialValues.email} onChange={(e) => handleInfoChange('email', e.target.value)} />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Password:</Form.Label>
                    <Form.Control defaultValue={(initialValues && initialValues.password) ? initialValues.password : 123345} onChange={(e) => handleInfoChange('password', e.target.value)} />
                </Form.Group>
            </React.Fragment>
            }
            <Button style={{ float: "right" }} type="Submit" onClick={SubmitEmployee}>{(initialValues? 'Update':'Add')}</Button>
        </Modal>)
}

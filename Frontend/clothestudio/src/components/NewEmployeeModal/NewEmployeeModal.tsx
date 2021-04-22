import axios from "axios";
import React, { ChangeEvent, useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { Employee } from "../../models/Employee";
import { Modal } from "../Modal/Modal";

interface ModalProps {
    modalIsOpen: boolean;
    handleClose(): void;
    handleFormSubmit(): void;
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
    handleFormSubmit
}: ModalProps) => {
    const [positions, setPositions] =useState([])
    const [selectedOption, setSelectedOption] = useState(0);
    const [newEmp, setNewEmp] = useState<Employee>({  atelieID: (JSON.parse(localStorage.getItem('user')!).atelieID) as number} as Employee)
    
    const handleInfoChange = (property: string, e: ChangeEvent<HTMLInputElement>) => {
        setNewEmp(prev => ({ ...prev, [`${property}`]: e.target.value }))
    }

    useEffect (()=>{
        const res =  axios.get('http://localhost:3000/api/Positions').then(r=> setPositions(r.data))
        console.log(positions)
    },[])

    const SubmitEmployee = () => {
        const res = axios.post('http://localhost:3000/api/Employees',newEmp).then(r => r.data)
        console.log(res)
    }

    return (
        <Modal isOpen={modalIsOpen} handleCloseModal={handleClose} style={customStyles}>
            <Form.Group>
                <Button variant='primary'>Sun</Button>
            <Form.Control as="select"  
                onChange={(e:any)=>handleInfoChange('positionID', e)} >
              {positions && positions.map((p:{positionID:number,name:string})=> <option value={p.positionID }>{p.name}</option>)}
                </Form.Control>
                </Form.Group>
            Firstname<input onChange={(e) => handleInfoChange('firstname', e)} />
           Lastname<input onChange={(e) => handleInfoChange('lastname', e)} />
           Email<input onChange={(e) => handleInfoChange('email', e)} />
           phonenumb<input onChange={(e) => handleInfoChange('phoneNumber', e)} />
           password<input onChange={(e) => handleInfoChange('password', e)} />
            <button onClick={SubmitEmployee}>SAve to console</button>
            {newEmp.createDate && newEmp.createDate.toString()}
            <button onClick={handleClose}>ikcdcf</button>
        </Modal>)
}

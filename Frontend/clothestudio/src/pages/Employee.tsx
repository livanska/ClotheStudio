import axios from "axios";
import React, { useEffect, useState } from "react";
import { Tabs, Tab, Button } from "react-bootstrap";
import { EmployeeCard } from "../components/EmployeeCard/EmployeeCard";
import { NewEmployeeModal } from "../components/NewEmployeeModal/NewEmployeeModal";

export const Employee = () => {
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const openModal = () => setModalIsOpen(true);
    const [employees, setEmployees] = useState<any[]>()
    const [key, setKey] = useState<string>("Department");
    const [initialValues, setInitialValues] = useState<any>({})
    const closeModal = () => {
        // setCurrentlyEditedField(null);
        setModalIsOpen(false);
    };

    useEffect(() => {
        const res = axios.get('http://localhost:3000/api/Employees').then(r => setEmployees(r.data))
    }, [modalIsOpen])

    const handleEdit = (emp:any) => {
        setInitialValues(emp)
        console.log(emp)
        openModal()
    }

    return (
        <div>
            <Tabs id="uncontrolled-tab-example"
                activeKey={key}
                onSelect={(k) => k ? setKey(k) : setKey("My Requests")}>
                {/* <Tab eventKey="All" title="All">
        <div >Employees in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
          <div style ={{display:"flex" ,flexWrap:'wrap', justifyContent:"space-between"}} > {employees && employees.map(e => <EmployeeCard {...e} />)  } {employees && employees.map(e => <EmployeeCard {...e} />)  }</div> 

        </div>
        </Tab> */}
                <Tab eventKey="Department" title={(JSON.parse(localStorage.getItem('user')!)).atelie}>
                    <div ><div style={{ marginTop: '10px', marginBottom: '10px' }}>Employees in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:   <Button style={{ 'float': 'right', marginTop: '-3px' }} className=' mb-0 mr-2' size='sm' onClick={openModal} >Add new employee</Button></div>
                        <div style={{ display: "flex", flexWrap: 'wrap', justifyContent: "space-between" }} > {employees ? employees.map(e => <EmployeeCard {...{...e}} wantEdit={() => handleEdit(e)} />) : <p>Loading</p>}</div>
                    </div>
                </Tab>
            </Tabs>
            <NewEmployeeModal
                modalIsOpen={modalIsOpen}
                handleClose={closeModal}
                handleFormSubmit={closeModal}
                initialValues={initialValues}
            />
        </div>

    )
}


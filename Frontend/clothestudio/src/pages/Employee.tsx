import axios from "axios";
import React, { useEffect, useState } from "react";
import { Tabs, Tab, Button } from "react-bootstrap";
import { EmployeeCard } from "../components/EmployeeCard/EmployeeCard";
import { NewEmployeeModal } from "../components/NewEmployeeModal/NewEmployeeModal";
import HighchartsReact from 'highcharts-react-official'
import { PieChart } from "react-minimal-pie-chart";

export const Employee = () => {
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const openModal = () => setModalIsOpen(true);
    const [employees, setEmployees] = useState<any[]>()
    const [key, setKey] = useState<string>("Employees");
    const [initialValues, setInitialValues] = useState<any>({})
    const [atelie, setAtelie] = useState<any>({})
    const closeModal = () => {
        // setCurrentlyEditedField(null);
        setModalIsOpen(false);
    };

    useEffect(() => {
        const res = axios.get(`http://localhost:3000/api/Atelies/${(JSON.parse(localStorage.getItem('user')!)).atelieID}`).then(r => setAtelie(r.data.Result))

    }, [])

    useEffect(() => {
        const res = axios.get('http://localhost:3000/api/Employees').then(r => setEmployees(r.data))
    }, [modalIsOpen])

    const handleEdit = (emp: any) => {
        setInitialValues(emp)
        console.log(emp)
        openModal()
    }

    return (
        <div>
            <Tabs id="uncontrolled-tab-example"
                activeKey={key}
                onSelect={(k) => k ? setKey(k) : setKey("My Requests")}>
       
                <Tab eventKey="Employees" title='Employees'>
                    <div ><div style={{ marginTop: '10px', marginBottom: '10px' }}>Employees in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:   <Button style={{ 'float': 'right', marginTop: '-3px' }} className=' mb-0 mr-2' size='sm' onClick={openModal} >Add new employee</Button></div>
                        <div style={{ display: "flex", flexWrap: 'wrap', gap:"25px"}} > {employees ? employees.filter((e:any)=>(JSON.parse(localStorage.getItem('user')!)).atelieID == e.atelieID ).map(e => <EmployeeCard {...{ ...e }} wantEdit={() => handleEdit(e)} />) : <p>Loading</p>}</div>
                    </div>
                </Tab>
                {atelie && <Tab eventKey="Stats" title='Stats'>
                <h4 style={{ marginTop: '10px', marginBottom: '10px' }}>Stats of {(JSON.parse(localStorage.getItem('user')!)).atelie} department: </h4>
                  <hr/><b  style={{ fontSize: '22px', marginTop: '10px' }}>Outgoings: { atelie.totalOutgoings && atelie.totalOutgoings.toFixed(2)}$<br/>Income: { atelie.totalIncome && atelie.totalIncome.toFixed(2)}$</b>  <PieChart
                        label={({ dataEntry }) => (dataEntry.value && dataEntry.value.toFixed(2) + '$')}
                        radius={PieChart.defaultProps.radius - 7}
                        segmentsShift={(index) => (index === 0 ? 7 : 0.5)}
                        labelStyle={{ fontSize: '5px', fill:'white' }}
                        style={{ height: '400px' }}
                        animate={true}
                        data={[
                            { title: 'Income', value: atelie.totalIncome, color: '#52abff' },
                            { title: 'Outgoings', value: atelie.totalOutgoings, color: '#1b3c5c' },

                        ]} />
                </Tab>}
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


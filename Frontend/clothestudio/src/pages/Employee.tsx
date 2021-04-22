import axios from "axios";
import React, { useEffect, useState } from "react";
import { EmployeeCard } from "../components/EmployeeCard/EmployeeCard";
import { NewEmployeeModal } from "../components/NewEmployeeModal/NewEmployeeModal";

export const Employee = () => {
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const openModal = () => setModalIsOpen(true);
    const closeModal = () => {
     // setCurrentlyEditedField(null);
      setModalIsOpen(false);
    };
    const [employees, setEmployees] = useState<any[]>()
    useEffect(()=>{
          const res =  axios.get('http://localhost:3000/api/Atelies?id=55').then(r=> setEmployees(r.data.Result.employees))

    },[])
    return (
        
        <div>Employees in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
            {employees && employees.map(e => <EmployeeCard {...e}/>)}
            <NewEmployeeModal
            //field={currentlyEditedField}
            modalIsOpen={modalIsOpen}
            handleClose={closeModal}
            handleFormSubmit={closeModal} />
            <button  onClick={openModal} >cscscs</button>
        </div>
        
    )
}


import axios from "axios";
import React, { useEffect, useState } from "react";
import { EmployeeCard } from "../components/EmployeeCard/EmployeeCard";

export const Employee = () => {
    const [employees, setEmployees] = useState<any[]>()
    useEffect(()=>{
          const res =  axios.get('http://localhost:3000/api/Atelies?id=55').then(r=> setEmployees(r.data.Result.employees))

    },[])
    return (
        <div>Employees in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
            {employees && employees.map(e => <EmployeeCard {...e}/>)}
        </div>
    )
}


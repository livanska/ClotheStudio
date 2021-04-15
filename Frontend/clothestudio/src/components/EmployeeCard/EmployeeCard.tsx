export const EmployeeCard =(emp:any)=>{
    return(
        <div>
           <p> {emp.firstname} {emp.lastname}</p> 
           <p> {emp.position}</p> 
           <p>{(new Date(Date.parse(emp.createDate) )).toLocaleDateString()}</p>
           <p>{emp.email}</p>
           <p>{emp.phoneNumber}</p>
        </div>
    )
}
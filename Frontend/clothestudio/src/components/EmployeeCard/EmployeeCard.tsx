import axios from "axios"
import { Button, Card } from "react-bootstrap"

export const EmployeeCard = (e:any, wantEdit:any) => {
const deleteEmpl=(e:number)=>{
    const res = axios.delete(`http://localhost:3000/api/Employees/${e}`).then( r=> alert("Deleted employee!"))
}

    return (
        <Card style={{ width: '32%' }} >
            <Card.Body>
                <Card.Title className="pt-0 pb-0 mb-0">{e.firstname} {e.lastname}</Card.Title>
                <Card.Subtitle className="mb-1 mt-0 pt-0 text-muted">{e.position}</Card.Subtitle>
                <Card.Text>
                    <span>Email: {e.email}</span>
                    <br />
                    <span>Phone: {e.phoneNumber}</span>
                    <br />
                    <span>Start date: {(new Date(Date.parse(e.createDate))).toLocaleDateString()}</span>
                 
                </Card.Text  >
                { (e.position== 'Master'||e.position== 'Department Manager')&& 
                
                <span className='pt-0 mt-0'>
                    <hr style={{ marginTop: '0px', marginBottom: '2px'}}/>
                Orders count: { e.ordersCount }<br/>
                Total profit: { e.ordersCost?e.ordersCost:0 }$<br/>
                Avarage order cost: { e.avgOrderCost? e.avgOrderCost.toFixed(2): 0 }$
                </span>}
                <hr style={{ marginTop: '4px', marginBottom: '10px'}}/>
                <Card.Subtitle style={{marginBottom:'20px', fontSize: '12px' }}>Department: {e.atelie}</Card.Subtitle>
                <Button size='sm' style={{ position: 'absolute',bottom:'5px' ,left:'150px'}} onClick={()=>e.wantEdit(e)}>Edit</Button>
                <Button size='sm' style={{ position: 'absolute',bottom:'5px',left:'200px' }} onClick={()=>deleteEmpl(e.employeeID)}>Delete</Button>
            </Card.Body>
        </Card>

    )
}

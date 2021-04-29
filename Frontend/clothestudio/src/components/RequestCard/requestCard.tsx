import axios from "axios"
import React, { ChangeEvent, useEffect, useState } from "react"
import { Accordion, Button, Card, Col, Form, Row } from "react-bootstrap"
import { Order } from "../../models/Order"


export const RequestCard = (props: any, statuses:any, deleteReq:any) => {
    const [request, setRequest] =useState<any>(props.props)

    async function handleStatusChange ( e: ChangeEvent<HTMLInputElement>) {

        const res = await axios.put(`http://localhost:3000/api/Requests/${request.requestID}`,{...request,statusID:e.target.value}).then(r => r.data).then(r=> alert("Status changed!"))
        console.log(res)
        setRequest((prev:any)=>({...prev,statusID:e.target.value}))
    }

     const deleteRequest=(request:any)=>{
        props.deleteReq(request)     
    }

    return (
        request && (
            <div className="container-sm mb-1 mt-1">
             
        <Card>
        <Accordion.Toggle as={Card.Header} eventKey={(request.RequestPayment.Payment.billNumber)}>
          
             {request.SupplierCompany.name}   {request.SupplierCompany.rating}/100    
            <Row style={{'float':'right'}}  > 
            <span  className="mr-4"><b>{request.RequestPayment.Payment.totalCost.toFixed(2)} $</b> </span>
                <span>
            <Form.Control  as="select"  value={request && request.statusID}
                 onChange={(e:any)=>handleStatusChange(e)} defaultValue={1}>
              {props.statuses && props.statuses.map((p:any) => <option value={p.requestStatusID }>{p.name}</option>)}
              </Form.Control></span>
              <Button className="ml-5 " style={{'float':'right'}} onClick={()=>deleteRequest(request)}>Delete</Button>
                </Row> 
            </Accordion.Toggle>
            <Accordion.Collapse eventKey={(request.RequestPayment.Payment.billNumber)}>
            <Card.Body className="m-0 p-0 pl-2">
                <Card.Title className="m-0 p-0" >Items:</Card.Title>
                <Card.Text className="m-0 p-0">
                   {request.RequestedMaterials && request.RequestedMaterials.map((oi:any,ind:number)=><p>{ind+1}. {oi.Material.Color.name} {oi.Material.MaterialType.name}: {oi.amount}units {' x '} {oi.Material.costPerUnit}$   <p style={{'float':'right'}} className="mr-5"> <b>{Math.round(oi.amount*oi.Material.costPerUnit)}$</b></p> </p>)}
                   <Card.Subtitle style={{paddingBottom:'5px', fontSize: '12px' }}>Employee: {request.Employee.firstname} {request.Employee.lastname}</Card.Subtitle>
                </Card.Text>
            </Card.Body>
            </Accordion.Collapse>
        </Card>
        </div>)
    )
}
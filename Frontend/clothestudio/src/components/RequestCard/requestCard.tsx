import axios from "axios"
import React, { ChangeEvent, useEffect, useState } from "react"
import { Accordion, Button, Card, Col, Form, Row } from "react-bootstrap"
import { Order } from "../../models/Order"


export const RequestCard = (props: any, statuses:any, deleteReq:any) => {
    const [request, setRequest] =useState<any>(props.props)
    console.log(props)

    async function handleStatusChange ( e: ChangeEvent<HTMLInputElement>) {
        // setOrder((prev:any)=>({...prev,statusID:e.target.value}))
        
        const res = await axios.put(`http://localhost:3000/api/Requests/${request.requestID}`,{...request,statusID:e.target.value}).then(r => r.data)
        console.log(res)
        setRequest((prev:any)=>({...prev,statusID:e.target.value}))
    }

    async function handleDoneOrder(){
        setRequest((prev:any)=>({...prev, statusID:4}))
        const res = await axios.put(`http://localhost:3000/api/Requests/${request.requestID}`,request).then(r => r.data)
    }
     const deleteRequest=(request:any)=>{
    // const res = await axios.put(`http://localhost:3000/api/Requests/${request.requestID}`,request).then(r => r.data)
    props.deleteReq(request)     
    }
    // async function handleDoneItem (item:any){

    //     if(item.doneTime == undefined)
    //     {
    //      item.doneTime = (new Date(Date.now()))
    //      //setDone(true);
    //     }
    //     else
    //     {
    
    //         item.doneTime = undefined
    //     }
    //     const res = await axios.put(`http://localhost:3000/api/OrderedItems/${item.orderedItemID}`,item).then(r => r.data)
 

    //     setOrder((prev:any)=>{
    //         const orderedItems = prev.OrderedItems
    //         orderedItems.map((el:any)=> (el.orderedItemID == item.orderedItemID && item)) 
    //         return {...prev, OrderedItems:orderedItems}
    //     })

 
    //}
    return (
        request && (
            <div className="container-sm mb-1 mt-1">
             
        <Card>
        <Accordion.Toggle as={Card.Header} eventKey={(request.RequestPayment.Payment.billNumber)}>
          
             {request.SupplierCompany.name}   {request.SupplierCompany.rating}    
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
                </Card.Text>
            </Card.Body>
            </Accordion.Collapse>
        </Card>
        </div>)
    )
}
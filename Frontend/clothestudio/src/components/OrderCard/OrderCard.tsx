import axios from "axios"
import React, { ChangeEvent, useEffect, useState } from "react"
import { Accordion, Button, Card, Col, Form, Row } from "react-bootstrap"
import { Order } from "../../models/Order"


export const OrderCard = (props: any, statuses:any) => {
    const [order, setOrder] =useState<any>(props.props)
    console.log(order)

    async function handleStatusChange ( e: ChangeEvent<HTMLInputElement>) {
        // setOrder((prev:any)=>({...prev,statusID:e.target.value}))
        const res = await axios.put(`http://localhost:3000/api/Orders/${order.orderID}`,{...order,statusID:e.target.value}).then(r => r.data)
        setOrder((prev:any)=>({...prev,statusID:e.target.value}))
    }

    async function handleDoneOrder(){
        setOrder((prev:any)=>({...prev, statusID:4}))
        const res = await axios.put(`http://localhost:3000/api/Orders/${order.orderID}`,order).then(r => r.data)
    }

    async function handleDoneItem (item:any){

        if(item.doneTime == undefined)
        {
         item.doneTime = (new Date(Date.now()))
         //setDone(true);
        }
        else
        {
    
            item.doneTime = undefined
        }
        const res = await axios.put(`http://localhost:3000/api/OrderedItems/${item.orderedItemID}`,item).then(r => r.data)
 

        setOrder((prev:any)=>{
            const orderedItems = prev.OrderedItems
            orderedItems.map((el:any)=> (el.orderedItemID == item.orderedItemID && item)) 
            return {...prev, OrderedItems:orderedItems}
        })

 
    }
    return (
        order && (
            <div className="container-sm">
             
        <Card>
        <Accordion.Toggle as={Card.Header} eventKey={(order.OrderPayment.Payment.billNumber)}>
          
            {order.Customer.firstname} {order.Customer.lastname}   {order.Customer.phoneNumber}
            <span style={{'float':'right'}}> 
            <Form.Control as="select"  value={order && order.OrderStatus.statusID}
                 onChange={(e:any)=>handleStatusChange(e)} defaultValue={1}>
              {props.statuses && props.statuses.map((p:any) => <option value={p.orderStatusID }>{p.name}</option>)}
                </Form.Control></span>
            </Accordion.Toggle>
            <Accordion.Collapse eventKey={(order.OrderPayment.Payment.billNumber)}>
            <Card.Body>
                <Card.Title>Items:</Card.Title>
                <Card.Text>
                   {order.OrderedItems.map((oi:any)=><p>{oi.Services.ServiceType.name} {oi.Services.ClotheType.name}<Button onClick={()=>handleDoneItem(oi)}>Set Done</Button>{oi.doneTime? oi.doneTime.toString():'dcd'}</p>)}
                </Card.Text>
            </Card.Body>
            </Accordion.Collapse>
        </Card>
        </div>)
    )
}
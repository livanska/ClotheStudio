import axios from "axios"
import React, { ChangeEvent, useEffect, useState } from "react"
import { Accordion, Button, Card, Col, Form, Row } from "react-bootstrap"
import { Order } from "../../models/Order"


export const OrderCard = (props: any, statuses: any, deleteOrd:any) => {
    const [order, setOrder] = useState<any>(props.props)

    console.log(order)
    async function handleStatusChange(e: ChangeEvent<HTMLInputElement>) {
        const res = await axios.put(`http://localhost:3000/api/Orders/${order.orderID}`, { ...order, statusID: e.target.value }).then(r => r.data).then(r=> alert("Status changed!"))
        setOrder((prev: any) => ({ ...prev, statusID: e.target.value }))
    }

    // async function handleDoneOrder() {
    //     setOrder((prev: any) => ({ ...prev, statusID: 4 }))
    //     const res = await axios.put(`http://localhost:3000/api/Orders/${order.orderID}`, order).then(r => r.data)
    // }

    async function handleDoneItem(item: any) {

        if (item.doneTime == undefined || !item.doneTime) {
            item.doneTime = new Date(Date.now()).toDateString()
        }
        else {

            item.doneTime = undefined
        }
        const res = await axios.put(`http://localhost:3000/api/OrderedItems/${item.orderedItemID}`, item).then(r => r.data).then(r=> alert("Status changed!"))


        setOrder((prev: any) => {
            const orderedItems = prev.OrderedItems
            console.log(orderedItems)
            orderedItems.map((el: any) => (el.orderedItemID == item.orderedItemID && item))
            return { ...prev, OrderedItems: orderedItems }
        })

  


    }
    const deleteOrder=(order:any)=>{
     props.deleteOrd(order)     
    }
    return (
        order && (
            <div className="container-sm mb-1 mt-1">

                <Card>
                    <Accordion.Toggle as={Card.Header} eventKey={(order.OrderPayment.Payment.billNumber)}>

                        {order.Customer.firstname} {order.Customer.lastname}   {order.Customer.phoneNumber}
                        <Row style={{ 'float': 'right' }}  >
                            <span className="mr-4"><b>{order.OrderPayment.Payment.totalCost.toFixed(2)} $</b> </span>
                            <span>
                                <Form.Control as="select" value={order && order.statusID}
                                    onChange={(e: any) => handleStatusChange(e)} defaultValue={1}>
                                    {props.statuses && props.statuses.map((p: any) => <option value={p.orderStatusID}>{p.name}</option>)}
                                </Form.Control></span>
                            <Button className="ml-5 " style={{ 'float': 'right' }} onClick={() => deleteOrder(order)}>Delete</Button>
                        </Row>
                    </Accordion.Toggle>
                    <Accordion.Collapse eventKey={(order.OrderPayment.Payment.billNumber)}>
                        <Card.Body className="m-0 p-0 pr-2 pl-2" >
                            <Card.Title className="m-0 p-0" >Items:</Card.Title>
                            <Card.Text className="m-0 p-0" >
                            {order.OrderedItems.map((oi: any, ind: number) => <p className='mb-4'>{ind + 1}. {oi.Services.ServiceType.name} {oi.Services.ClotheType.name}  {oi.doneTime && ( <span style={{marginLeft:"10px"}}>Done at: {new Date(Date.parse(oi.doneTime)).toLocaleString()}</span>)}
                <span style={{ float: 'right' }}>{(oi.Services.workCost + ((oi.RequiredMaterialsForOrderedItem.reduce((sum:number, rm:any) => sum + rm.amount * rm.Material.costPerUnit, 0)))).toFixed(2)} $ 
                <Button style={{ float: 'right' }} size='sm' className='ml-2' onClick={() => handleDoneItem(oi)}>Set Done</Button></span>
                <br />{oi.RequiredMaterialsForOrderedItem.map((rm: any) => (<span style={{ backgroundColor: '#9ccaff', borderRadius: '4px', padding: '1px', margin: '1px' }}>{rm.Material.MaterialType.name}</span>))}
               
            </p>)}                <Card.Subtitle style={{paddingBottom:'5px', fontSize: '12px' }}>Employee: {order.Employee.firstname} {order.Employee.lastname}</Card.Subtitle>

                                {/* {order.OrderedItems.map((oi: any) => <p>{oi.Services.ServiceType.name} {oi.Services.ClotheType.name}
                                    <span>{oi.doneTime ? oi.doneTime.toString() : ''}<Button style={{ 'float': 'right' }} onClick={() => handleDoneItem(oi)}>Set Done</Button></span></p>)} */}
                            </Card.Text>
                        </Card.Body>
                    </Accordion.Collapse>
                </Card>
            
            </div>)
    )
    
}


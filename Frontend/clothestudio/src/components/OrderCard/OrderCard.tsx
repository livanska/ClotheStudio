import React from "react"
import { Accordion, Button, Card, Col, Row } from "react-bootstrap"
import { Order } from "../../models/Order"


export const OrderCard = (props: any) => {
    console.log(props)
    return (
        props && (
            <div className="container-sm">
             
        <Card>
        <Accordion.Toggle as={Card.Header} eventKey={(props.OrderPayment.Payment.billNumber)}>
          
            {props.Customer.firstname} {props.Customer.lastname}   {props.Customer.phoneNumber}
            <span style={{'float':'right'}}>{props.OrderStatus.name}</span>
            </Accordion.Toggle>
            <Accordion.Collapse eventKey={(props.OrderPayment.Payment.billNumber)}>
            <Card.Body>
                <Card.Title>Items:</Card.Title>
                <Card.Text>
                   {props.OrderedItems.map((oi:any)=><p>{oi.description}</p>)}
                </Card.Text>
                <Button variant="primary">Go somewhere</Button>
            </Card.Body>
            </Accordion.Collapse>
        </Card>
        </div>)
    )
}
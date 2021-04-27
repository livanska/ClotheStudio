import axios from 'axios'
import React, { ChangeEvent, useEffect, useMemo, useState } from 'react'
import { Col, Form, FormControlProps, InputGroup, Row, Button } from 'react-bootstrap'
import { NewOrderedItem } from '../../components/NewOrderedItemModal/NewOrderedItemModal'
import { defaultOrder, Order } from '../../models/Order'
import { OrderedItem } from '../../models/OrderedItem'

export const NewOrder = () => {
    const [order, setOrder] = useState<Order>(defaultOrder)
    const [orderedItems, setOrderedItems] = useState<any>([])
    const [customer, setCustomer] = useState<any>({})
    const [needNewCustomer, setNeedNewCustomer] = useState<boolean>(false)
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const openModal = () => setModalIsOpen(true);
    const closeModal = () => {
        // setCurrentlyEditedField(null);
        setModalIsOpen(false);
    };


    const createOrder = () => {
        const res = axios.post('http://localhost:3000/api/Orders', { ...order, totalCost: total }).then(r => r.data)
        console.log(order)
    }

    useEffect(() => {
        handleInfoSelect('employeeID', (JSON.parse(localStorage.getItem('user')!).employeeID) as number)
        handleInfoSelect('atelieID', (JSON.parse(localStorage.getItem('user')!).atelieID) as number)
    }, [])


    const handleDateChange = (e: string) => {
        //431-630-6441
       // new Date(e).toDateString()
        let date = new Date(e).toDateString()
        setOrder(prev => ({ ...prev, expectedDeadlineTime: date }))
    }

    const handleInfoSelect = (property: string, e: number) => {
        setOrder(prev => ({ ...prev, [`${property}`]: e }))
    }
    async function handleOrdersAmount(id:number){
        const res = await axios.get(`http://localhost:3000/api/Customers/${id}`).then(r=>r.data)
        setCustomer(res.Result)
    }

    async function handleNumberChange(number: string) {
        const res = await axios.get(`http://localhost:3000/api/Customers?number=${number}`).then(r => { setNeedNewCustomer(false); handleOrdersAmount(r.data.customerID) }).catch(function (error) {
            if (error.response) {
                setNeedNewCustomer(true)
                setCustomer((prev: any) => ({ ...prev, phoneNumber: number }))
            }
            else {
                setNeedNewCustomer(false)
            }
        })
    }

    useEffect(() => {
        setOrder(prev => ({ ...prev, orderedItems: orderedItems }))
    }, [orderedItems])

    useEffect(() => {
        if (!needNewCustomer)
            handleInfoSelect('customerID', customer.customerID)
        else setOrder(prev => ({ ...prev,customerID:-1, firstname: customer.firstname, lastname: customer.lastname, phoneNumber: customer.phoneNumber }))
    }, [customer])

    const deleteOrderItem = (oi: OrderedItem) => {
        setOrderedItems((prev: OrderedItem[]) => prev.filter(o => o !== oi))
    }

    const total = useMemo(() => {
        let sum = orderedItems.reduce((s: number, oi: OrderedItem) => s + oi.serviceCost + ((oi.reqMaterials.reduce((sum, rm) => sum + rm.materialAmount * rm.materialCost, 0))), 0)
        console.log(sum)
        return (customer.customerID !== undefined) ? sum - (sum * customer.discount / 100) : sum
    }, [orderedItems, customer])

    return (
        <div style={{ textAlign: 'left' }}>
         <span>   <Form.Label className=' p-2' >New order in {(JSON.parse(localStorage.getItem('user')!)).atelie} department: </Form.Label><Button style={{ float: 'right' }} className='ml-2 mb-2' disabled={orderedItems.length == 0} onClick={createOrder}>Create order</Button></span>
            <InputGroup className="mb-3 mt-2">
                <InputGroup.Prepend>
                    <InputGroup.Text>Customer phone number:</InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control  placeholder='012-345-6789' onChange={(e) => handleNumberChange(e.target.value)}></Form.Control>
            </InputGroup>
            {(customer.customerID && !needNewCustomer )&& <p><b>Customer: </b><Form.Label>{customer.firstname } {customer.lastname}  </Form.Label><b  style={{ float: 'right' }}> Discount: {customer.discount ? customer.discount :'0' } %</b>
            <br /><b><p style={{ float: 'right' }}>{'Orders: '}{customer.ordersCount}</p></b> </p> }
            {needNewCustomer && <Form.Group >
                <Form.Label>New customer personal information:</Form.Label>
                <Form>
                    <Row>
                        <Col>
                            <Form.Control placeholder='Firstname' onChange={(e) => (setCustomer((prev: any) => ({ ...prev, firstname: e.target.value })))} />
                        </Col>
                        <Col>
                            <Form.Control placeholder='Lastname' onChange={(e) => (setCustomer((prev: any) => ({ ...prev, lastname: e.target.value })))} />
                        </Col>
                    </Row>
                </Form>
            </Form.Group>}
            <Form.Group >
                <Form.Label>Expected receiving date</Form.Label>
                <Form.Control type="datetime-local" placeholder="Deadline" onChange={(e) => handleDateChange(e.target.value)} />
            </Form.Group>

            {orderedItems.map((oi: OrderedItem, ind: number) => <p className='mb-4'>{ind + 1}. {oi.serviceName}
                <span style={{ float: 'right' }}>{oi.serviceCost + (Math.round(oi.reqMaterials.reduce((sum, rm) => sum + rm.materialAmount * rm.materialCost, 0)))} $</span>
                <br />{oi.reqMaterials.map((rm: any) => (<span style={{ backgroundColor: '#9ccaff', borderRadius: '4px', padding: '1px', margin: '1px' }}>{rm.materialName}</span>))}
                <Button style={{ float: 'right' }} size='sm' className='ml-2' onClick={() => deleteOrderItem(oi)}>Delete</Button>
            </p>)}
            <div>
                {orderedItems.length !== 0 && <p className='mb-4'> <hr /> <Form.Label style={{ float: 'right' }}><b>Total: {total.toFixed(2)} $</b></Form.Label></p>}

            </div> <NewOrderedItem
                items={orderedItems}
                addToOrder={setOrderedItems}
                modalIsOpen={modalIsOpen}
                handleClose={closeModal}
                handleFormSubmit={closeModal} />
            <div className='mt-4' style={{ textAlign: 'center' }}>
                <Button className='mt-4 ml-5' onClick={openModal} >Add item to order</Button>
            </div>
        </div>
    )
}

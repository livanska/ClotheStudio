import axios from 'axios'
import React, { ChangeEvent, useEffect, useMemo, useState } from 'react'
import { Form, FormControlProps, InputGroup } from 'react-bootstrap'
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


    const createOrder=()=>{
        const res = axios.post('http://localhost:3000/api/Orders', {...order,totalCost:total}).then(r=>r.data)
        console.log(order)
    }

    useEffect(() => {
        handleInfoSelect('employeeID', (JSON.parse(localStorage.getItem('user')!).employeeID) as number)
        handleInfoSelect('atelieID', (JSON.parse(localStorage.getItem('user')!).atelieID) as number)
    }, [])


    const handleDateChange = (e:string)=>{
        //431-630-6441
        let date = ((new Date(Date.parse(e) )).toLocaleString())
        setOrder(prev => ({ ...prev, expectedDeadlineTime: date}))
    }

    const handleInfoSelect = (property: string, e: number) => {
        setOrder(prev => ({ ...prev, [`${property}`]: e }))
    }

    async function handleNumberChange (number: string){
        const res = await axios.get(`http://localhost:3000/api/Customers?number=${number}`).then(r => { setNeedNewCustomer(false); setCustomer(r.data) }).catch(function (error) {
            if (error.response) {
                setNeedNewCustomer(true)
                setCustomer((prev: any) => ({ ...prev, phoneNumber: number }))
            }
            else {
                setNeedNewCustomer(false)
            }
        })
    }

    useEffect(()=>{
        setOrder(prev=>({ ...prev, orderedItems:orderedItems}))
    },[orderedItems])

    useEffect(()=>{
        if(!needNewCustomer)
            handleInfoSelect('customerID', customer.customerID)
       else setOrder(prev=> ({...prev, firstname:customer.firstname , lastname:customer.lastname ,phoneNumber:customer.phoneNumber}))
    },[customer])

    const deleteOrderItem = (oi: OrderedItem) => {
        setOrderedItems((prev: OrderedItem[]) => prev.filter(o => o !== oi))
    }

    const total = useMemo(() => {
        let sum = orderedItems.reduce((s: number, oi: OrderedItem) => s + oi.serviceCost + (Math.round(oi.reqMaterials.reduce((sum, rm) => sum + rm.materialAmount * rm.materialCost, 0))), 0)
        console.log(sum)
        return (customer.customerID !== undefined) ? sum - Math.round(sum * customer.discount / 100) : sum
    }, [orderedItems, customer])

    return (
        <div>Orders in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
            <Form.Group >
                <Form.Label>Customer phone number:</Form.Label>
                <Form.Control onChange={(e) => handleNumberChange(e.target.value)}></Form.Control>
            </Form.Group>
            {needNewCustomer && <div>
                <Form.Label>No such customer. Create new one:</Form.Label>
                <InputGroup className="mb-3">
                    <InputGroup.Prepend>
                        <InputGroup.Text>First and last name</InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control onChange={(e) => (setCustomer((prev: any) => ({ ...prev, firstname: e.target.value })))} />
                    <Form.Control onChange={(e) => (setCustomer((prev: any) => ({ ...prev, lastname: e.target.value })))} />
                </InputGroup>
            </div>}

            {customer && <p>{customer.firstname} {customer.firstname && (customer.discount ? `${customer.discount}%` : 0)}</p>}
            <Form.Group >
                <Form.Label>Select Date</Form.Label>
                <Form.Control type="datetime-local" placeholder="Deadline" onChange={(e) => handleDateChange( e.target.value)} />
            </Form.Group>
            <button onClick={() => { console.log(order) }}>console</button>
            {orderedItems.map((oi: OrderedItem) => <p>{oi.serviceName}
                {oi.serviceCost + (Math.round(oi.reqMaterials.reduce((sum, rm) => sum + rm.materialAmount * rm.materialCost, 0)))}$
            <button onClick={() => deleteOrderItem(oi)}>Delete</button>
            <button onClick={createOrder}>Create Order</button>
            </p>)}
            {orderedItems.length !== 0 && <p>Total:{total}</p>}
            <NewOrderedItem
                items={orderedItems}
                addToOrder={setOrderedItems}
                modalIsOpen={modalIsOpen}
                handleClose={closeModal}
                handleFormSubmit={closeModal} />
            <button onClick={openModal} >addOrderItem</button>
        </div>
    )
}

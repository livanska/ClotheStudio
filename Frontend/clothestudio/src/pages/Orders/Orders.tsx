import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Accordion } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import { OrderCard } from '../../components/OrderCard/OrderCard'
import { NewOrder } from '../NewOrder/NewOrder'

export const Orders = () => {
    const [orders, setOrders] = useState<any[]>()
    useEffect(() => {
        const res = axios.get(`http://localhost:3000/api/Orders?atelieNum=${(JSON.parse(localStorage.getItem('user')!)).atelieID}&employeeNum=${(JSON.parse(localStorage.getItem('user')!)).employeeID}`).then(r => setOrders(r.data))
        console.log(res)
    }, [])
    return (
        <div>Orders in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
        {orders  ?   <Accordion>{(orders.map(e => (<OrderCard {...e }/>)))}</Accordion>  : <p>Loading...</p>}
        <Link to='newOrder'>New</Link>
        </div>
    )
}
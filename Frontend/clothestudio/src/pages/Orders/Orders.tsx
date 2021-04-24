import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { NewOrder } from '../NewOrder/NewOrder'

export const Orders = () => {
    const [orders, setOrders] = useState<any[]>()
    useEffect(() => {
        const res = axios.get('http://localhost:3000/api/Orders?atelieNum=68&employeeNum=54').then(r => setOrders(r.data))
        console.log(res)
    }, [])
    return (
        <div>Orders in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
            {orders ? orders.map(e => (<div>{e.orderID}{e.OrderedItems && e.OrderedItems.map((oi: { orderedItemID: number }) => (<p>evdv{oi.orderedItemID}</p>))}   </div>)) : <p>Loading...</p>}
        <Link to='newOrder'>New</Link>
        </div>
    )
}
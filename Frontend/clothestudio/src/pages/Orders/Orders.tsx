import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Accordion, Tabs, Tab, Button } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import { OrderCard } from '../../components/OrderCard/OrderCard'
import { NewOrder } from '../NewOrder/NewOrder'

export const Orders = () => {
    const [orders, setOrders] = useState<any[]>()
    const [orderStatuses, setOrderStatuses] = useState<any[]>()
    const [key, setKey] = useState<string>("My Orders");

    async function deleteOrder(e: any) {
        orders && setOrders((prev: any) => prev.filter((r: any) => r.orderID !== e.orderID))
        const res = await axios.delete(`http://localhost:3000/api/Orders/${e.orderID}`).then(r => r.data).then(r=> alert("Order deleted!"))
    }

    useEffect(() => {
        const res1 = axios.get(`http://localhost:3000/api/OrderStatus`).then(r => setOrderStatuses(r.data))
        console.log(res1)
    }, [])

    useEffect(() => {
        setOrders(undefined)
        if (key == "My Orders" || key == "My History")
            axios.get(`http://localhost:3000/api/Orders?atelieNum=${(JSON.parse(localStorage.getItem('user')!)).atelieID}&employeeNum=${(JSON.parse(localStorage.getItem('user')!)).employeeID}`).then(r => setOrders(r.data))
        else
            axios.get(`http://localhost:3000/api/Orders`).then(r => setOrders(r.data))

    }, [key])

    return (<div>
        
        <Tabs id="uncontrolled-tab-example"
            activeKey={key}
            onSelect={(k) => k ? setKey(k) : setKey("My Requests")}>
                            {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 &&
            <Tab eventKey="All" title="All">
                <h5  className='mt-2'>Orders in all departments: <Link  to='newOrder'>{ 'Create new ->'}</Link></h5>
                {orders ? <Accordion>{((orders.filter((r: any) => (r.statusID !== 5))).map(e => (<OrderCard key={e.orderID} props={e} statuses={orderStatuses} deleteOrd={() => deleteOrder(e)} />)))}</Accordion> : <p>Loading...</p>}
            </Tab>}
            {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 &&
            <Tab eventKey="Department" title="Department">
                <h5  className='mt-2'>Orders in {(JSON.parse(localStorage.getItem('user')!)).atelie} department: <Link  to='newOrder'>{ ' Create new ->'}</Link></h5>
                {(JSON.parse(localStorage.getItem('user')!)).atelie} department: <Link to='newOrder'>New</Link>
            {orders ? <Accordion>{(orders.filter((r: any) => ((r.statusID !== 5) && (r.Employee.atelieID == (JSON.parse(localStorage.getItem('user')!)).atelieID))).map(e => (<OrderCard key={e.orderID} props={e} statuses={orderStatuses} deleteOrd={() => deleteOrder(e)} />)))}</Accordion> : <p>Loading...</p>}
            </Tab>}
        <Tab eventKey="My Orders" title="My Orders">
            <h5  className='mt-2 mb-2'>Orders by {(JSON.parse(localStorage.getItem('user')!).position)}  {(JSON.parse(localStorage.getItem('user')!).firstname)} {(JSON.parse(localStorage.getItem('user')!).lastname)}:<Link  to='newOrder'>{ ' Create new ->'}</Link></h5>
            {orders ? <Accordion>{((orders.filter((r: any) => (r.statusID !== 5))).map(e => (<OrderCard key={e.orderID} props={e} statuses={orderStatuses} deleteOrd={() => deleteOrder(e)} />)))}</Accordion> : <p>Loading...</p>}
        </Tab>
        {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 &&
        <Tab eventKey="All History" title="All History">
            <h5  className='mt-2'>Orders done in all departments:<Link  to='newOrder'>{ ' Create new ->'}</Link></h5>
            {orders ? <Accordion>{((orders.filter((r: any) => (r.statusID == 5))).map(e => (<OrderCard key={e.orderID} props={e} statuses={orderStatuses} deleteOrd={() => deleteOrder(e)} />)))}</Accordion> : <p>Loading...</p>}
        </Tab>}
        {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 &&
        <Tab eventKey="Department History" title="Department History">
        <h5 className='mt-2' >Orders done in {(JSON.parse(localStorage.getItem('user')!)).atelie} department: <Link  to='newOrder'>{ ' Create new ->'}</Link></h5>
            {orders ? <Accordion>{(orders.filter((r: any) => ((r.statusID == 5) && (r.Employee.atelieID == (JSON.parse(localStorage.getItem('user')!)).atelieID))).map(e => (<OrderCard key={e.orderID} props={e} statuses={orderStatuses} deleteOrd={() => deleteOrder(e)} />)))}</Accordion> : <p>Loading...</p>}
        </Tab>}
        <Tab eventKey="My History" title="My History">
        <h5  className='mt-2'>Orders done by {(JSON.parse(localStorage.getItem('user')!).firstname)} {(JSON.parse(localStorage.getItem('user')!).lastname)}: <Link  to='newOrder'>{ ' Create new ->'}</Link></h5>
            {orders ? <Accordion>{((orders.filter((r: any) => (r.statusID == 5))).map(e => (<OrderCard key={e.orderID} props={e} statuses={orderStatuses} deleteOrd={() => deleteOrder(e)} />)))}</Accordion> : <p>Loading...</p>}
        </Tab><Button style={{float:'right'}}><Link to='newOrder'>New</Link></Button>
            
        </Tabs>
        </div >
    )
}
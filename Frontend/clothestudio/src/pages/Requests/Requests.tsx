import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Accordion, Tab, Tabs } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import { RequestCard } from '../../components/RequestCard/requestCard'

export const Requests = () => {
    const [requests, setRequests] = useState<any[]>()
    const [requestStatuses, setRequestStatuses] = useState<any[]>()
    const [key, setKey] = useState<string>("My Requests");


    async function deleteRequest(e: any) {
        requests && setRequests((prev: any) => prev.filter((r: any) => r.requestID !== e.requestID))
        const res = await axios.delete(`http://localhost:3000/api/Requests/${e.requestID}`).then(r => r.data).then(r=> alert("Request deleted!"))
    }

    useEffect(() => {
        const res1 = axios.get(`http://localhost:3000/api/RequestStatus`).then(r => setRequestStatuses(r.data))
        console.log(res1)
    },[])


    useEffect(() => {
        setRequests(undefined)
        if (key == "My Requests" || key == "My History")
        axios.get(`http://localhost:3000/api/Requests?atelieNum=${(JSON.parse(localStorage.getItem('user')!)).atelieID}&employeeNum=${(JSON.parse(localStorage.getItem('user')!)).employeeID}`).then(r => setRequests(r.data))
       else
            axios.get(`http://localhost:3000/api/Requests`).then(r => setRequests(r.data))
       
    }, [key])

    return (
        <Tabs id="uncontrolled-tab-example"
            activeKey={key}
            onSelect={(k) => k ? setKey(k) : setKey("My Requests")}>
           {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 && <Tab eventKey="All" title="All">
            <h5  className='mt-2'>Requests in all departments: <Link  to='newRequest'>{ 'Create new ->'}</Link></h5>
                {requests ? <Accordion>{((requests.filter((r:any)=> ( r.statusID !== 4  ))).map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
            </Tab>}
            {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 &&
            <Tab eventKey="Department" title="Department">
            <h5  className='mt-2'>Requests in {(JSON.parse(localStorage.getItem('user')!)).atelie} department: <Link  to='newRequest'>{ ' Create new ->'}</Link></h5>
                {requests ? <Accordion>{(requests.filter((r:any)=> (( r.statusID !== 4 )&&( r.Employee.atelieID == (JSON.parse(localStorage.getItem('user')!)).atelieID))).map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
             
            </Tab>}
            <Tab eventKey="My Requests" title="My Requests">
            <h5  className='mt-2 mb-2'>Requests by {(JSON.parse(localStorage.getItem('user')!).position)}  {(JSON.parse(localStorage.getItem('user')!).firstname)} {(JSON.parse(localStorage.getItem('user')!).lastname)}:<Link  to='newRequest'>{ ' Create new ->'}</Link></h5>
                {requests ? <Accordion>{((requests.filter((r:any)=> ( r.statusID !== 4  ))).map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
      
            </Tab>
            {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 &&
            <Tab eventKey="All History" title="All History">
            <h5  className='mt-2'>Requests done in all departments: <Link  to='newRequest'>{ 'Create new ->'}</Link></h5>
                {requests ? <Accordion>{((requests.filter((r:any)=> ( r.statusID == 4  ))).map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
                    <Link to='newRequest'>New Request</Link>
               
            </Tab>}
            {(JSON.parse(localStorage.getItem('user')!)).positionID == 0 &&
            <Tab eventKey="Department History" title="Department History">
            <h5  className='mt-2'>Requests done in {(JSON.parse(localStorage.getItem('user')!)).atelie} department: <Link  to='newRequest'>{ ' Create new ->'}</Link></h5>
                {requests ? <Accordion>{(requests.filter((r:any)=> (( r.statusID == 4 )&&( r.Employee.atelieID == (JSON.parse(localStorage.getItem('user')!)).atelieID))).map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
            </Tab>}
            <Tab eventKey="My History" title="My History">
            <h5  className='mt-2 mb-2'>Requests done by {(JSON.parse(localStorage.getItem('user')!).position)}  {(JSON.parse(localStorage.getItem('user')!).firstname)} {(JSON.parse(localStorage.getItem('user')!).lastname)}:<Link  to='newRequest'>{ ' Create new ->'}</Link></h5>
                {requests ? <Accordion>{((requests.filter((r:any)=> ( r.statusID == 4 ))).map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
        
       
            </Tab>
        </Tabs>
    )
}
import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Accordion, Tab, Tabs } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import { RequestCard } from '../../components/RequestCard/requestCard'

export const Requests = () => {
    const [requests, setRequests] = useState<any[]>()
    const [requestStatuses, setRequestStatuses] = useState<any[]>()
    const [key, setKey] = useState<string>("Done");

    async function deleteRequest(e: any) {
        requests && setRequests((prev: any) => prev.filter((r: any) => r.requestID !== e.requestID))
        const res = await axios.delete(`http://localhost:3000/api/Requests/${e.requestID}`).then(r => r.data)
    }

    useEffect(() => {
        const res1 = axios.get(`http://localhost:3000/api/RequestStatus`).then(r => setRequestStatuses(r.data))
        console.log(res1)
    },[])


    useEffect(() => {
        setRequests(undefined)
        if(key == "All" || "Done")
            axios.get(`http://localhost:3000/api/Requests`).then(r => setRequests(r.data))
        else if (key == "Department")
            axios.get(`http://localhost:3000/api/Requests?atelieNum=${(JSON.parse(localStorage.getItem('user')!)).atelieID}`).then(r => setRequests(r.data))
        else if (key == "My Requests")
            axios.get(`http://localhost:3000/api/Requests?atelieNum=${(JSON.parse(localStorage.getItem('user')!)).atelieID}&employeeNum=${(JSON.parse(localStorage.getItem('user')!)).employeeID}`).then(r => setRequests(r.data))
       
    }, [key])

    return (
        <Tabs id="uncontrolled-tab-example"
            activeKey={key}
            onSelect={(k) => k ? setKey(k) : setKey("My Requests")}>
            <Tab eventKey="All" title="All">
                <div>Requests in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
                {requests ? <Accordion>{(requests.map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
                    <Link to='newRequest'>New Request</Link>
                </div>
            </Tab>
            <Tab eventKey="Department" title="Department">
                <div>Requests in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
                {requests ? <Accordion>{(requests.map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
                    <Link to='newRequest'>New Request</Link>
                </div>
            </Tab>
            <Tab eventKey="My Requests" title="My Requests">
                <div>Requests in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
                {requests ? <Accordion>{(requests.map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
                    <Link to='newRequest'>New Request</Link>
                </div>
            </Tab>
            <Tab eventKey="Done" title="Done">
                <div>Requests in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
                {requests ? <Accordion>{((requests.filter((r:any)=> ( r.statusID == 4 || r.statusID == 3 ))).map(e => (<RequestCard key={e.requestID} props={e} statuses={requestStatuses} deleteReq={() => deleteRequest(e)} />)))}</Accordion> : <p>Loading...</p>}
                    <Link to='newRequest'>New Request</Link>
                </div>
            </Tab>
        </Tabs>
    )
}
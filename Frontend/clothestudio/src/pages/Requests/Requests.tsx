import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'

export const Requests = () => {
    const [requests, setRequests] = useState<any[]>()
    useEffect(() => {
        const res = axios.get(`http://localhost:3000/api/Requests?atelieNum=${(JSON.parse(localStorage.getItem('user')!)).atelieID}&employeeNum=${(JSON.parse(localStorage.getItem('user')!)).employeeID}`).then(r => setRequests(r.data))
        console.log(res)
    }, [])
    return (
        <div>Requests in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
            {requests ? requests.map(e => (<div>{e.orderID}</div>)) : <p>Loading...</p>}
            <Link to='newRequest'>New Request</Link>
        </div>
    )
}
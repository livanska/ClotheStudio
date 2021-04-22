import axios from 'axios'
import React, { useEffect, useState } from 'react'

export const Requests = () => {
    const [requests, setRequests] = useState<any[]>()
    useEffect(() => {
        const res = axios.get('http://localhost:3000/api/Orders?atelieNum=68').then(r => setRequests(r.data))
        console.log(res)
    }, [])
    return (
        <div>Requests in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
            {requests ? requests.map(e => (<div>{e.orderID}</div>)) : <p>Loading...</p>}
        </div>
    )
}
import axios from "axios"
import React, { useEffect, useState } from "react"
import { Button, Table } from "react-bootstrap"
import * as _ from 'lodash'

export const Storage = () => {
    const [stored, setStored] = useState<any[]>()

    useEffect(() => {
        const res1 = axios.get(`http://localhost:3000/api/StoredMaterials`).then(r => setStored(r.data))
        console.log(res1)
    }, [])


    return (
        <div><span>Materials in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:</span><span style={{ float: 'right', marginBottom: '5px' }}><Button onClick={() => setStored(prev => (_.orderBy(Array.from(prev!), 'Material.MaterialType.name')))} variant="outline-primary">Sort by name</Button>
            <Button onClick={() => setStored(prev => (_.orderBy(Array.from(prev!), 'Material.Color.name')))} variant="outline-primary">Sort by color</Button></span>
            {stored ?
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <td>Material name</td>
                            <td>Color</td>
                            <td>Amount</td>
                            <td>Location</td>
                        </tr>
                    </thead>
                    <tbody>
                        {stored && (stored.filter((st: any) => st.Atelie.atelieID == (JSON.parse(localStorage.getItem('user')!)).atelieID))
                            .map((a: any) => <tr>
                                <td>{a.Material.MaterialType.name}</td>
                                <td>{a.Material.Color.name}</td>
                                <td>{a.amountLeft}</td>
                                <td>{a.Atelie.address}, {a.Atelie.City.name}, {a.Atelie.City.Country.name}</td>
                            </tr>)}
                    </tbody>
                </Table>
                : <p>Loading...</p>}
            {(stored && stored.length == 0) && <h5>No materials in this department</h5>}

        </div>)
}
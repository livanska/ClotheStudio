import axios from 'axios'
import React, { ChangeEvent, useEffect, useMemo, useState } from 'react'
import { Form, FormControlProps, InputGroup, Button } from 'react-bootstrap'
import { defaultRequest, Request } from '../../models/Request'


export const NewRequest = () => {

    const [materials, setMaterials] = useState<any[]>()
    const [noMaterial, setNoMaterial] = useState<boolean>(false)

    useEffect(() => {
        axios.get('http://localhost:3000/api/Materials').then(r => setMaterials(r.data))

    }, [])

    const [request, setRequest] = useState<Request>(defaultRequest)
    const [requestedMaterials, setRequestedMaterials] = useState<any[]>([])
    const [company, setCompany] = useState<any>({})
    const [needNewCompany, setNeedNewCompany] = useState<boolean>(false)
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const openModal = () => setModalIsOpen(true);
    const closeModal = () => {
        // setCurrentlyEditedField(null);
        setModalIsOpen(false);
    };


    const createRequest = () => {
        console.log(total)
        const res = axios.post('http://localhost:3000/api/Requests', { ...request, totalCost: total }).then(r => r.data)
        //console.log(res)
        //console.log( { ...request, totalCost: total })
    }

    useEffect(() => {
        handleInfoSelect('employeeID', (JSON.parse(localStorage.getItem('user')!).employeeID) as number)
        handleInfoSelect('atelieID', (JSON.parse(localStorage.getItem('user')!).atelieID) as number)
    }, [])


    const handleDateChange = (e:string)=>{
        //431-630-6441
        let date = ((new Date(Date.parse(e) )).toLocaleString())
        setRequest(prev => ({ ...prev, expectedDeadlineTime: date}))
    }

    const handleInfoSelect = (property: string, e: number) => {
        setRequest(prev => ({ ...prev, [`${property}`]: e }))
    }

    async function handleLocation(data:any) {
        const city = await axios.get(`http://localhost:3000/api/Cities?id=${data.cityID}`).then(r => r.data)
        const country = await axios.get(`http://localhost:3000/api/Cities?id=${city.countryID}`).then(r => r.data)
        
        setCompany({...data , city: `${city.name}`, country:`${country.name}` })
        
    }

    async function handleNumberChange(name: string) {
        const res = await axios.get(`http://localhost:3000/api/SupplierCompanies?name=${name}`).then(r => { setNeedNewCompany(false); handleLocation(r.data);}).catch(function (error) {
            if (error.response) {
                setNeedNewCompany(true)
                setRequest((prev: any) => ({ ...prev, name: name }))
            }
            else {
                setNeedNewCompany(false)
            }
        })
    }

    useEffect(() => {
        setRequest(prev => ({ ...prev, requestedMaterials: requestedMaterials }))
    }, [requestedMaterials])

    useEffect(() => {
        if (!needNewCompany)
            handleInfoSelect('companyID', company.companyID)
        else {
            setRequest(prev => ({ ...prev, city: company.city, country: company.country, address: company.address }))
        }
    }, [company])

    async function handleMaterialChange(s: number) {
        const mater = await axios.get(`http://localhost:3000/api/Materials?id=${s}`).then(r => r.data.Result)
        setRequestedMaterials(prev => ([...prev, { materialID: s as number, materialName: `${mater.color} ${mater.name}`, materialCost: mater.costPerUnit, materialAmount: 1 as number  }]));

    }
    const changeAmount = (id: number, e: any) => {
        setRequestedMaterials(prev =>
            prev.map(rm => {
                if (rm.materialID === id) {
                    return {
                        ...rm,
                        materialAmount: e.target.value as number
                    };
                }
                return rm;
            })
        );
    }

    const deleteAll = () => {
        setRequestedMaterials([])
    }
    const handleSelect = (e: any) => {
        setNoMaterial(false)
        requestedMaterials.some(s => s['materialID'] == e.target.value) ? setRequestedMaterials(prev => prev.filter(s => s.materialID !== e.target.value)) : handleMaterialChange(e.target.value)

    }

    const total = useMemo(() => {
        let sum = requestedMaterials.reduce((sum, rm) => sum + rm.materialAmount * rm.materialCost, 0)
        return Math.round(sum)
    }, [requestedMaterials])

    return (
        <div>Orders in {(JSON.parse(localStorage.getItem('user')!)).atelie} department:
            <Form.Group >
                <Form.Label>Customer phone number:</Form.Label>
                <Form.Control onChange={(e) => handleNumberChange(e.target.value)}></Form.Control>
            </Form.Group>
            {needNewCompany && <div>
                <Form.Label>No such company. Create new one:</Form.Label>
                <InputGroup className="mb-3">
                    <InputGroup.Prepend>
                        <InputGroup.Text>Address, city, country:</InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control onChange={(e) => (setCompany((prev: any) => ({ ...prev, address: e.target.value })))} />
                    <Form.Control onChange={(e) => (setCompany((prev: any) => ({ ...prev, city: e.target.value })))} />
                    <Form.Control onChange={(e) => (setCompany((prev: any) => ({ ...prev, country: e.target.value })))} />
                </InputGroup>
            </div>}

            {company && <p>{company.name} {company.rating} {company.city}</p>}
            <Form.Group >
                <Form.Label>Select Date</Form.Label>
                <Form.Control type="datetime-local" placeholder="Deadline" onChange={(e) => handleDateChange(e.target.value)} />
            </Form.Group>
            <Form.Group>
                <Form.Label>Select Materials<Button onClick={() => deleteAll()}>Clean</Button></Form.Label>
                <Form.Control as="select" multiple={true}
                    value={requestedMaterials.map(a => a.materialID)}
                    onChange={(e: any) => { handleSelect(e) }}>
                    {materials && materials.map((p: { materialID: number, name: string, color: string }) => <option value={p.materialID}>{p.color} {p.name}</option>)}
                </Form.Control>
            </Form.Group>
            <Form.Group>
                {requestedMaterials.map(rm =>
                    <InputGroup size="sm" className="mb-2 center" >
                        <InputGroup.Prepend>
                            <InputGroup.Text id="inputGroup-sizing-sm" aria-setsize={5}>{rm.materialName} units:</InputGroup.Text>
                        </InputGroup.Prepend>
                        <Form.Control type='number' min={1} onChange={(e) => changeAmount(rm.materialID, e)} defaultValue={1} aria-label="Small" aria-describedby="inputGroup-sizing-sm" />
                        <InputGroup.Append>
                            <InputGroup.Text>{rm.materialAmount <= 0 ? Math.round(0) : (Math.round(rm.materialCost * rm.materialAmount))}$</InputGroup.Text>
                        </InputGroup.Append>
                    </InputGroup>)}
            </Form.Group>
            <button onClick={createRequest}>Create Order</button>
            <button onClick={() => { console.log(request) }}>console</button>
            {requestedMaterials.length !== 0 && <p>Total:{total}</p>}
        </div>
    )
}

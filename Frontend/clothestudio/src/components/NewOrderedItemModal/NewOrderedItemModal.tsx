import axios from "axios";
import React, { ChangeEvent, useEffect, useState } from "react";
import { Button, Form, InputGroup } from "react-bootstrap";
import { Employee } from "../../models/Employee";
import { defaultOrderedItem, OrderedItem } from "../../models/OrderedItem";
import { Modal } from "../Modal/Modal";

interface ModalProps {
    modalIsOpen: boolean;
    handleClose(): void;
    handleFormSubmit(): void;
    addToOrder(items: any): any;
    items: any
}
const customStyles: ReactModal.Styles = {
    content: {
        inset: '50% auto auto 50%',
        top: '50%',
        right: 'auto',
        bottom: 'auto',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: '100%',
        maxWidth: '540px'
    }
};

export const NewOrderedItem = ({ modalIsOpen,
    handleClose,
    handleFormSubmit,
    addToOrder,
    items,
}: ModalProps) => {

    const [materials, setMaterials] = useState<any[]>()
    const [services, setServices] = useState<any[]>()
    const [reqMaterials, setReqMaterials] = useState<any[]>([])
    const [noMaterial, setNoMaterial] = useState<boolean>(false)
    const [item, setItem] = useState<OrderedItem>(defaultOrderedItem)

    useEffect(() => {
        axios.get('http://localhost:3000/api/Materials').then(r => setMaterials(r.data))
        axios.get('http://localhost:3000/api/Services').then(r => setServices(r.data))
    }, [])

    async function handleInfoChange(property: string, e: ChangeEvent<HTMLInputElement>) {
        setItem(prev => ({ ...prev, [`${property}`]: e.target.value }))
        if (property == 'serviceID') {
            const serv = await axios.get(`http://localhost:3000/api/Services?id=${e.target.value}`).then(r => r.data.Result)
            await handleServiceChange(serv)
        }
    }

    useEffect(() => {
        setItem(prev => ({ ...prev, reqMaterials: reqMaterials }))
    }, [reqMaterials])

    async function handleMaterialChange(s: number) {
        const mater = await axios.get(`http://localhost:3000/api/Materials?id=${s}`).then(r => r.data.Result)
        setReqMaterials(prev => ([...prev, { materialID: s, materialName: `${mater.color} ${mater.name}`, materialCost: mater.costPerUnit }]));

    }

    function handleServiceChange(serv: any) {
        setItem(prev => ({ ...prev, serviceName: serv.name, serviceCost: serv.workCost }));
    }

    async function SubmitOrderedItem() {
        console.log(item)
        addToOrder([...items, item])
        setReqMaterials([])
    }
    const handleSelect = (e: any) => {
        if (e.target.value == 'No materials') {
            setNoMaterial(true)
            setReqMaterials([])
        }
        else {
            setNoMaterial(false)
            reqMaterials.some(s => s['materialID'] == e.target.value) ? setReqMaterials(prev => prev.filter(s => s.materialID !== e.target.value)) : handleMaterialChange(e.target.value)
        }
    }
    const changeAmount = (id: number, e: any) => {
        setReqMaterials(prev =>
            prev.map(rm => {
                if (rm.materialID === id) {
                    return {
                        ...rm,
                        materialAmount: e.target.value
                    };
                }
                return rm;
            })
        );
    }

    return (
        <Modal isOpen={modalIsOpen} handleCloseModal={handleClose} style={customStyles}>
            <Form.Group>
                <Form.Control as="select" multiple={true}
                    value={noMaterial ? ['No materials'] : reqMaterials.map(a => a.materialID)}
                    onChange={(e: any) => { handleSelect(e) }}>
                    <option value={'No materials'}>No materials</option>
                    {materials && materials.map((p: { materialID: number, name: string, color: string }) => <option value={p.materialID}>{p.color} {p.name}</option>)}
                </Form.Control>
            </Form.Group>
            <Form.Group>
                <Form.Control as="select"
                    onChange={(e: any) => handleInfoChange('serviceID', e)} defaultValue={0}>
                    {services && services.map((p: { serviceID: number, name: string, workCost: number }) => <option value={p.serviceID}> {p.name} {p.workCost}$</option>)}
                </Form.Control>
            </Form.Group>
            {reqMaterials.map(rm => <p>{rm.materialName} <Form.Control type='number'
                onChange={(e) => changeAmount(rm.materialID, e)} defaultValue={0} ></Form.Control></p>)}
            <InputGroup>
                <InputGroup.Prepend>
                    <InputGroup.Text>Description</InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control onChange={(e: any) => handleInfoChange('description', e)} as="textarea" aria-label="With textarea" />
            </InputGroup>
            <button onClick={SubmitOrderedItem}>Add</button>
            <button onClick={handleClose}>close</button>
        </Modal>)
}


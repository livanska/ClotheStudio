import axios from "axios"
import React, { useState } from "react"
import { Button, Form } from "react-bootstrap"
import { Redirect } from "react-router"
import { Link, useHistory } from "react-router-dom"
import css from './Login.module.scss'
import ClotheStudio from './ClotheStudio.svg';

export const Login = () => {
    const [email, setEmail] = useState<string>()
    //localStorage.setItem('user', '')
    async function login() {
        const res = await axios.get(`http://localhost:3000/api/Employees?email=${email}&password=string`).then(r => r.data.Result);
        console.log(res)
        localStorage.setItem('user', JSON.stringify(res));

        if (res.position == 'Master' || 'Department Manager')
            window.location.href = '/orders'
        else
            window.location.pathname = '/requests'
    }



    return (
        <div className={css.page_content}>
            <div> <img src={ClotheStudio}></img> </div>
            
                <div className='container container-sm ' style={{ fontSize: '25px', textAlign: 'left' }}>

                    <Form.Group>
                        <Form.Label>Email:</Form.Label>

                        <Form.Control placeholder="Email" type="email" onChange={(e) => setEmail(e.target.value)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Password:</Form.Label>
                        <Form.Control placeholder="Password" type="password" />
                    </Form.Group>
                </div>
                <hr />
                <h5>If you dont have an account yet, ask Manager of your department to create it.</h5>
                <hr />
    

            <div>  <Button size='lg' onClick={login}>Login</Button>
            </div>

        </div>
    )
}

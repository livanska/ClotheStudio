import axios from "axios"
import React from "react"
import { Button } from "react-bootstrap"
import { Redirect } from "react-router"
import { Link } from "react-router-dom"
import css from './Login.module.scss'
import Logo from './Logo.svg';

export const Login = () => {
    localStorage.setItem('user','')
    async function login() {
        const res = await axios.get('http://localhost:3000/api/Employees?id=35').then(r=>r.data)
        console.log(res.Result.position)
        localStorage.setItem('user',JSON.stringify(res.Result));
       window.location.pathname = '/employees'
      }
    return (
        <div className={css.page_content}>Login
        <img src={Logo}></img>
                <Button onClick={login}>Login</Button>
              
        </div>
    )
}

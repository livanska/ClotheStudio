import axios from "axios"
import React from "react"
import { Redirect } from "react-router"
import { Link } from "react-router-dom"

export const Login = () => {
    localStorage.setItem('user','')
    async function login() {
        const res = await axios.get('http://localhost:3000/api/Employees?id=35').then(r=>r.data)
        console.log(res.Result.position)
        localStorage.setItem('user',JSON.stringify(res.Result));
       window.location.pathname = '/'
      }
    return (
        <div>Login
                <button onClick={login}>Login</button>
              
        </div>
    )
}

import React from 'react'
import { BrowserRouter as Router } from 'react-router-dom';
import { Login } from '../pages/Login/Login';
import { ManagerRouter, routes as managerRoutes } from './ManagerRouter';
import { MasterRouter, routes as masterRoutes } from './MasterRouter';
import { RequestManagerRouter, routes as requestManagerRoutes } from './RequestManagerRouter';


//const role = localStorage.getItem('user') !=='' ? (JSON.parse(localStorage.getItem('user')|| '')).position : '';

export const RootRouter = () => {
const role = localStorage.getItem('user') !=='' ? (JSON.parse(localStorage.getItem('user')|| '')).position : '';
console.log(role)
return(
     <Router>{role && role == 'Department Manager' ? <ManagerRouter />: (role == 'Master' ?<MasterRouter />:(role == 'Storage Manager'? <RequestManagerRouter/>:<Login/>))}
     </Router>
)
}
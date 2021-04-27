import React from 'react'
import { BrowserRouter as Router } from 'react-router-dom';
import { Login } from '../pages/Login/Login';
import { ManagerRouter, routes as managerRoutes } from './ManagerRouter';
import { MasterRouter, routes as masterRoutes } from './MasterRouter';
import { RequestManagerRouter, routes as requestManagerRoutes } from './RequestManagerRouter';

const IsDepartmentManager = (JSON.parse(localStorage.getItem('user')|| '')).position == 'Department Manager';
const IsStorageManager = (JSON.parse(localStorage.getItem('user')|| '')).position == 'Storage Manager';

export const routes = IsDepartmentManager ? managerRoutes: (IsStorageManager? requestManagerRoutes:masterRoutes);
const role = localStorage.getItem('user') !=='' ? (JSON.parse(localStorage.getItem('user')|| '')).position : '';

export const RootRouter = () => {
   console.log(role)
return(
     <Router>{role && role == 'Department Manager' ? (<ManagerRouter />): role == 'Master' ?<MasterRouter />:<Login/>}
     </Router>
)
}
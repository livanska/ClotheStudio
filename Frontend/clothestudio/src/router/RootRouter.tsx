import React from 'react'
import { BrowserRouter as Router } from 'react-router-dom';
import { Login } from '../pages/Login';
import { ManagerRouter, routes as managerRoutes } from './ManagerRouter';


const role = localStorage.getItem('user') !=='' ? (JSON.parse(localStorage.getItem('user')|| '')).position : '';

export const routes = managerRoutes ;
export const RootRouter = () => (
     <Router>{role == 'Department Manager' || !role ? <ManagerRouter />:<Login/>}</Router>

  );
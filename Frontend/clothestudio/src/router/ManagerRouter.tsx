import React from 'react';

import { Switch, Route } from 'react-router-dom';
import { Layout } from '../components/layout/Layout/Layout';
import { Employee } from '../pages/Employee';
import { Orders } from '../pages/Orders/Orders';
import { Requests } from '../pages/Requests/Requests';


import {EmployeesRoute , OrdersRoute,RequestsRoute} from './routes';

export const routes = [
  {
    path: EmployeesRoute,
    exact: true,
    title: 'Employees',
    component: Employee
  },
  {
    path: OrdersRoute,
    title: 'Orders',
    component: Orders
  },
  {
    path: RequestsRoute,
    title: 'Requests',
    component: Requests
  },
//   {
//     path: FormsRoute,
//     title: 'My Forms',
//     component: Forms
//   },
//   {
//     path: NotificationsRoute,
//     title: 'Notifications',
//     component: Notifications
//   },
//   {
//     path: NewFormRoute,
//     title: 'Create New Form',
//     component: NewForm
//   },
//   {
//     path: '*',
//     title: 'Not Found',
//     component: NotFound
//   }
];

export const ManagerRouter = () => (
  <Layout>
    <Switch>
      {routes.map(route => (
        <Route path={route.path} exact={route.exact} key={route.path} component={route.component} />
      ))}
    </Switch>
  </Layout>
);

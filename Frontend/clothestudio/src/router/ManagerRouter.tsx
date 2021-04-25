import React from 'react';

import { Switch, Route } from 'react-router-dom';
import { Layout } from '../components/layout/Layout/Layout';
import { Employee } from '../pages/Employee';
import { NewOrder } from '../pages/NewOrder/NewOrder';
import { NewRequest } from '../pages/NewRequest/NewRequest';
import { Orders } from '../pages/Orders/Orders';
import { Requests } from '../pages/Requests/Requests';


import {EmployeesRoute , NewOrderRoute, OrdersRoute,RequestsRoute, NewRequestRoute} from './routes';

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
  {
    path: NewOrderRoute,
    title: 'NewOrder',
    component: NewOrder
  },
  {
    path: NewRequestRoute,
    title: 'NewRequest',
    component: NewRequest
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

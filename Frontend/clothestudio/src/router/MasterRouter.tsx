import React from 'react';

import { Switch, Route } from 'react-router-dom';
import { Layout } from '../components/layout/Layout/Layout';
import { Employee } from '../pages/Employee';
import { Login } from '../pages/Login/Login';
import { NewOrder } from '../pages/NewOrder/NewOrder';
import { NewRequest } from '../pages/NewRequest/NewRequest';
import { Orders } from '../pages/Orders/Orders';
import { Requests } from '../pages/Requests/Requests';
import { Storage } from '../pages/Storage/Storage';


import { LoginRoute, NewOrderRoute, OrdersRoute, StorageRoute} from './routes';

export const routes = [

  {
    path: OrdersRoute,
    title: 'Orders',
    component: Orders
  },

  {
    path: NewOrderRoute,
    title: 'NewOrder',
    component: NewOrder
  },
  {
    path: StorageRoute,
    title: 'Storage',
    component: Storage
  },
  {
    path: LoginRoute,
    title: 'Login',
    component: Login
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

export const MasterRouter = () => (
  <Layout>
    <Switch>
      {routes.map(route => (
        <Route path={route.path}  key={route.path} component={route.component} />
      ))}
    </Switch>
  </Layout>
);
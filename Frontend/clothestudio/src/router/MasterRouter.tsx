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
    exact: true,
    component: Orders
  },

  {
    path: NewOrderRoute,
    title: 'NewOrder',
    exact: true,
    component: NewOrder
  },
  {
    path: StorageRoute,
    title: 'Storage',
    exact: true,
    component: Storage
  },
  {
    path: LoginRoute,
    title: 'Login',
    exact: true,
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
        <Route path={route.path} exact={route.exact}  key={route.path} component={route.component} />
      ))}
    </Switch>
  </Layout>
);
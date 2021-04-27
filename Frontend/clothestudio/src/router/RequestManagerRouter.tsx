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


import {RequestsRoute, NewRequestRoute, StorageRoute,LoginRoute} from './routes';

export const routes = [


  {
    path: RequestsRoute,
    exact: true,
    title: 'Requests',
    component: Requests
  },

  {
    path: NewRequestRoute,
    title: 'NewRequest',
    exact: true,
    component: NewRequest
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

export const RequestManagerRouter = () => (
<Layout>
    <Switch>
      {routes.map(route => (
        <Route path={route.path} exact={route.exact} key={route.path} component={route.component} />
      ))}
    </Switch>
  </Layout>
);
import React from 'react';
import { Button } from 'react-bootstrap';
import { matchPath, RouteProps, useLocation , Link, useHistory} from 'react-router-dom';
import { ManagerRouter, routes as managerRoutes } from '../../../router/ManagerRouter';
import { MasterRouter, routes as masterRoutes } from '../../../router/MasterRouter';
import { RequestManagerRouter, routes as requestManagerRoutes } from '../../../router/RequestManagerRouter';




import css from './Header.module.scss';

interface HeaderProps {
  sidebarBurger: React.ReactNode;
}

export const Header = ({ sidebarBurger }: HeaderProps) => {

   const IsDepartmentManager = (JSON.parse(localStorage.getItem('user')|| '')).position == 'Department Manager';
const IsStorageManager = (JSON.parse(localStorage.getItem('user')|| '')).position == 'Storage Manager';

const routes = IsDepartmentManager ? managerRoutes: (IsStorageManager? requestManagerRoutes:masterRoutes);


  let history =useHistory()
  const location = useLocation();
  const currentRoute = routes.find((route: string | string[] | RouteProps<string, { [x: string]: string | undefined; }>) => matchPath(location.pathname, route));

  const logOut =()=>{
    ///window.location.pathname = '/login'
    //history.push("/")
    localStorage.setItem('user','')
  }


  return (
    <header className={css.header}>
      <span className={css.header_pageTitle}>{currentRoute?.title}</span>
     <div  style={{float:'right' ,marginTop:'-15px', paddingTop:'-15px'}}> 
     <h5 className='p-0' style={{marginTop:'-15px', marginBottom:'-7px'}} >{(JSON.parse(localStorage.getItem('user')!).firstname)} {(JSON.parse(localStorage.getItem('user')!).lastname)}</h5>
    <p className='p-0' style={{ marginBottom:'-15px'}} ><span style={{ marginRight:'80px'}}>{localStorage.getItem('user') !=='' ? (JSON.parse(localStorage.getItem('user')|| '')).position : ''}</span><Button  onClick ={logOut} className='ml-5 'style={{ marginTop:'-30px'}} ><Link to ='/'></Link>Log out</Button></p> 
    </div>
      
      {sidebarBurger}


    </header>
  );
};
// (JSON.parse(localStorage.getItem('user')).position
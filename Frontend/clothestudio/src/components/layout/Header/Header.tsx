import React from 'react';
import { Button } from 'react-bootstrap';
import { matchPath, RouteProps, useLocation } from 'react-router-dom';

import { routes } from '../../../router/RootRouter';


import css from './Header.module.scss';

interface HeaderProps {
  sidebarBurger: React.ReactNode;
}

export const Header = ({ sidebarBurger }: HeaderProps) => {
  const location = useLocation();
  const currentRoute = routes.find((route: string | string[] | RouteProps<string, { [x: string]: string | undefined; }>) => matchPath(location.pathname, route));

  const logOut =()=>{
    localStorage.setItem('user','')
    window.location.pathname = '/login'
  }


  return (
    <header className={css.header}>
      <span className={css.header_pageTitle}>{currentRoute?.title}</span>
     <div  style={{float:'right' ,marginTop:'-15px', paddingTop:'-15px'}}> 
     <h5 className='p-0' style={{marginTop:'-15px', marginBottom:'-7px'}} >{(JSON.parse(localStorage.getItem('user')!).firstname)} {(JSON.parse(localStorage.getItem('user')!).lastname)}</h5>
    <p className='p-0' style={{ marginBottom:'-15px'}} >{localStorage.getItem('user') !=='' ? (JSON.parse(localStorage.getItem('user')|| '')).position : ''}<Button  onClick ={logOut}className='ml-5 'style={{ marginTop:'-30px'}} >Log out</Button></p> 
    </div>
      
      {sidebarBurger}


    </header>
  );
};
// (JSON.parse(localStorage.getItem('user')).position
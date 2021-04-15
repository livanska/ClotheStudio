import React from 'react';
import { matchPath, RouteProps, useLocation } from 'react-router-dom';

import { routes } from '../../../router/RootRouter';


import css from './Header.module.scss';

interface HeaderProps {
  sidebarBurger: React.ReactNode;
}

export const Header = ({ sidebarBurger }: HeaderProps) => {
  const location = useLocation();
  const currentRoute = routes.find((route: string | string[] | RouteProps<string, { [x: string]: string | undefined; }>) => matchPath(location.pathname, route));

  return (
    <header className={css.header}>
      <span className={css.header_pageTitle}>{currentRoute?.title}</span>

      {sidebarBurger}


    </header>
  );
};

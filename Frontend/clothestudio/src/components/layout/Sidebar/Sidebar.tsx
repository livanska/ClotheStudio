import React, { useEffect, useRef } from 'react';
import cn from 'classnames'
import { NavLink } from 'react-router-dom';
import { SidebarNavProps, SidebarNavLinkProps, SidebarProps } from './Sidebar.types';
import Logo from '../../../assets/images/logo.svg';
import css from './Sidebar.module.scss';
import {
  EmployeesRoute, OrdersRoute
} from '../../../router/routes';

const SidebarNav = ({ onLinkClick }: SidebarNavProps) => {
  const navLinks: SidebarNavLinkProps[] = [
    { to: EmployeesRoute, name: 'Employees', exact: true  },
    { to: OrdersRoute, name: 'Orders' },
    

  ];

  return (
    <nav className={css.sidebar_nav}>
      <ul className={css.sidebar_navList}>
        {navLinks.map(item => {
          const { icon, ...linkProps } = item;

          return (
            <li key={linkProps.to}>
              <NavLink {...linkProps} activeClassName={css.active} onClick={onLinkClick}>
                {icon}
                {linkProps.name}
              </NavLink>
            </li>
          );
        })}
      </ul>
    </nav>
  );
};

export const Sidebar = ({ className, isOpen, setIsOpen }: SidebarProps) => {
  const sidebar = useRef<HTMLElement | null>(null);

  useEffect(() => {
    const closeSidebar = ({ target }: MouseEvent) => {
      if (target instanceof Node && isOpen && !sidebar.current?.contains(target)) {
        setIsOpen(false);
      }
    };

    window.addEventListener('click', closeSidebar);

    return () => {
      window.removeEventListener('click', closeSidebar);
    };
  }, [isOpen]);

  return (
    <aside className={cn(className, css.sidebar)} ref={sidebar}>
      <header className={css.sidebar_header}>
        <img className={css.sidebar_logo} src={Logo} alt='My Forms Logo' />
      </header>
      <SidebarNav onLinkClick={() => setIsOpen(false)} />
    </aside>
  );
};

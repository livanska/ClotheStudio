import React, { useState } from 'react';
import { Header } from '../Header/Header';
import { Sidebar } from '../Sidebar/Sidebar';
import cn from 'classnames'
import Burger from '../../Burger/Burger';
import css from './Layout.module.scss';

interface LayoutProps {
  children?: React.ReactNode;
}

export const Layout = ({ children }: LayoutProps) => {
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

  return (
    <div className={css.page}>
      <Sidebar
        className={cn(css.page_sidebar, {
          [css.isOpen]: isSidebarOpen
        })}
        isOpen={isSidebarOpen}
        setIsOpen={setIsSidebarOpen}
      />

      <div className={css.page_inner}>
        <Header
          sidebarBurger={
            <Burger
              isOpen={isSidebarOpen}
              className={css.page_sidebarBurger}
              onClick={() => {
                setIsSidebarOpen(!isSidebarOpen);
              }}
            />
          }
        />

        <main className={css.page_content}>{children}</main>

       
      </div>
    </div>
  );
};
import React from 'react';

import cn from 'classnames';
import css from './Burger.module.scss';

interface BurgerProps {
  isOpen: boolean;
  className?: string;
  onClick?: () => void;
}

interface BurgerProps {
  isOpen: boolean;
  className?: string;
  onClick?: () => void;
}

const Burger = ({ isOpen, className, onClick }: BurgerProps) => (
  <button
    className={cn(className, css.burger, {
      [css.burger__open]: isOpen
    })}
    onClick={onClick}
  >
    <span>Show / hide sidebar</span>
  </button>
);

export default Burger;

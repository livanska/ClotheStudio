import { ReactElement } from 'react';

export interface SidebarProps {
  className?: string;
  isOpen: boolean;
  setIsOpen(value: boolean): void;
}

export interface SidebarNavProps {
  onLinkClick: () => void;
}
export interface SidebarNavLinkProps {
  to: string;
  name: string;
  exact?: boolean;
  icon?: ReactElement;
}

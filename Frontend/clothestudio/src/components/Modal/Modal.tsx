import React from 'react';
import ReactModal from 'react-modal';

interface ModalProps extends ReactModal.Props {
  children?: React.ReactNode;
  handleCloseModal(): void;
}

export const Modal = ({ children, handleCloseModal, ...props }: ModalProps) => {
  ReactModal.setAppElement('#root');

  return (
    <ReactModal {...props} onRequestClose={handleCloseModal}>
      {children}
    </ReactModal>
  );
};

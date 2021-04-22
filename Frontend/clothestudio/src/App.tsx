import React from 'react';
import logo from './logo.svg';
import './App.css';
import { RootRouter } from './router/RootRouter';
import { Login } from './pages/Login';

function App() {

  return (
    <div className="App">
     <RootRouter/>
    </div>
  );
}

export default App;

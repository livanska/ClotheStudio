import React from 'react';
import logo from './logo.svg';
import './App.css';
import { RootRouter } from './router/RootRouter';
import { Login } from './pages/Login/Login';
import { BrowserRouter as Router } from 'react-router-dom';

function App() {
  const role = localStorage.getItem('user');
  console.log(role)
  return (
    <div className="App">
     {/* {(!role || role=='') ? <Login /> : <RootRouter />}  */}
<RootRouter/>

    </div>
  );
}

export default App;

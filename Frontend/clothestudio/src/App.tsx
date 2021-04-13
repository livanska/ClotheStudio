import React from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios'

function App() {

  async function cl() {
    let config = {
      headers: {crossDomain: true},
    //params:{
   //   number:'334-892-7744'
   // }
    }
    
    
    const res = await axios.get('http://localhost:3000/api/Customers?number=334-892-7744',config)
    console.log(res)
  }
  return (
    <div className="App">
     <button onClick={cl}>gsedfvsd</button>
    </div>
  );
}

export default App;

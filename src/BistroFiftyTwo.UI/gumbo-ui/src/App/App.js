import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import LoginForm from '../components/loginForm';

class App extends Component {
  render() {
    return (
      <div>
      <header className="mui-appbar mui--z1">

      </header>
      <div id="content-wrapper" className="mui--text-center">
         <LoginForm />
      </div>
      <footer>
        <div className="mui-container mui--text-center">
        Recipe Manager
        </div>
      </footer>
      </div>
    );
  }
}

export default App;

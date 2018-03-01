import React, { Component } from 'react';
import { Route } from "react-router-dom";
import Menu from './components/menu';
import './App.css';
import Debug from "./pages/debug";
import About from "./pages/about";
import Editor from "./pages/editor";

class App extends Component {
  render() {
    return (
      <div className="App">
        <Menu />
       <div>
         <Route path="/editor" component={Editor} />
         <Route path="/debug" component={Debug} />
         <Route path="/about" component={About} />
       </div>
      </div>
    );
  }
}

export default App;

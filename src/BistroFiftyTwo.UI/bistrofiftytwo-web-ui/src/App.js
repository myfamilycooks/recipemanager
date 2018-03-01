import React, { Component } from "react";
import { Route } from "react-router-dom";
import Menu from "./components/menu";
import PrivateRoute from "./components/privateRoute";
import Debug from "./pages/debug";
import About from "./pages/about";
import Editor from "./pages/editor";
import Login from "./pages/login";

class App extends Component {
  render() {
    return (
      <div className="App">
        <Menu />
        <div className="container">
          <PrivateRoute path="/editor" component={Editor} />
          <PrivateRoute path="/debug" component={Debug} />
          <Route path="/about" component={About} />
          <Route path="/login" component={Login} />
        </div>
      </div>
    );
  }
}

export default App;

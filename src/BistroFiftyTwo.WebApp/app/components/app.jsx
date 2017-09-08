import React, { Component } from 'react';

import Portal from './portal/landing';
import Login from './auth/login';
import Logout from './auth/logout';
import Register from './auth/register';
import { Switch, Route, Link } from 'react-router-dom'; 

export default class App extends Component {
  render() {
    return (

      <div>
                <Switch>          
                    <Route exact path="/logout" component={Logout} />
                    <Route exact path="/register" component={Register} /> 
                    <Route exact path="/login" component={Login} /> 
                    <Route exact path="/" component={Portal} />                     
                </Switch>
    </div>
    );
  }
}

import React, { Component } from 'react';

import Portal from './portal/landing';
import Login from './auth/login';
import Logout from './auth/logout';
import Register from './auth/register';
import Nav from './portal/nav';
import RecipeEditor from './pages/recipeEditor';
import RecipeDisplay from './pages/recipeDisplay';

import { Switch, Route, Link } from 'react-router-dom';
 
export default class App extends Component {
  render() {
    return (
      <div>
        <Route path="/portal" component={Nav} />
        <Switch> 
          <Route exact path="/logout" component={Logout} />
          <Route exact path="/register" component={Register} />
          <Route exact path="/login" component={Login} />
          <Route exact path="/portal/recipe/editor" component={RecipeEditor} />
          <Route exact path="/portal" component={Portal} />
          <Route exact path="/portal/recipe/:recipeId" component={RecipeDisplay} />
          <Route exact path="/" component={Portal} />
          
        </Switch>
      </div>
    );
  }
}
 
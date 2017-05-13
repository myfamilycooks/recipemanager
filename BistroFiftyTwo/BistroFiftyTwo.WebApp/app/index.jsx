import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware } from 'redux';
import { Router, Route, IndexRoute, hashHistory } from 'react-router';
import reduxthunk from 'redux-thunk';
import App from './components/app';
import Login from './components/auth/login';
import Logout from './components/auth/logout';
import Register from './components/auth/register';
import Landing from './components/portal/landing';
import reducers from './reducers';

const createStoreWithMiddleware = applyMiddleware(reduxthunk)(createStore);

ReactDOM.render(
  <Provider store = { createStoreWithMiddleware(reducers) }>
    <Router history = { hashHistory }>
      <Route path = "/" component = { App }>
        <Route path="/login" component={Login} />
        <Route path="/logout" component={Logout} />
        <Route path="/register" component={Register} />
        <Route path="/portal" component={Landing} />
      </Route>
    </Router>
  </Provider>, document.querySelector('.app'));

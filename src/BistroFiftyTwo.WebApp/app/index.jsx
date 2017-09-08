import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware } from 'redux';
import { Router, HashRouter, BrowserRouter, Route, IndexRoute } from 'react-router-dom';  
import reduxthunk from 'redux-thunk';
import App from './components/app';
import reducers from './reducers';

const createStoreWithMiddleware = applyMiddleware(reduxthunk)(createStore);

ReactDOM.render(
  <Provider store={createStoreWithMiddleware(reducers)}>
    <HashRouter basename="/">
      <Route path="/" component={App} />
    </HashRouter>
  </Provider>, document.querySelector('.app'));

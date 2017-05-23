import { createStore, combineReducers, applyMiddleware, compose } from 'redux';

import createHistory from 'history/createBrowserHistory'
import { syncHistoryWithStore, routerReducer, routerMiddleware } from 'react-router-redux';
import createOidcMiddleware, { createUserManager } from 'redux-oidc';
import reducer from './reducer';
import userManager from './utils/userManager';

// create the middleware with the userManager
const oidcMiddleware = createOidcMiddleware(userManager);
const history = createHistory();
const initialState = {};

const createStoreWithMiddleware = compose(
    applyMiddleware(oidcMiddleware, routerMiddleware(history))
)(createStore);

const store = createStoreWithMiddleware(reducer, initialState);

export default store;
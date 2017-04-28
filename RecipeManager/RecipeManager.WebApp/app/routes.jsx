import React from 'react';
import { createBrowserHistory } from 'history';
import { Router, Route, IndexRoute } from 'react-router';
import store from './store';
import { syncHistoryWithStore } from 'react-router-redux';
import HomePage from './components/homePage';
import CallbackPage from './components/callback';

const history = syncHistoryWithStore(createBrowserHistory(), store);

export default function Routes(props) {
    return ( 
    <Router history = { history }>
        <div>
        <Route path = "/" component = { HomePage }/>
        <Route path = "/callback" component = { CallbackPage }/>
        </div>
    </ Router>
    );
}
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import { App } from './App';
import registerServiceWorker from './registerServiceWorker';

ReactDOM.render(<App />, document.getElementById('root'));
registerServiceWorker();
// based on http://jasonwatmore.com/post/2017/09/16/react-redux-user-registration-and-login-tutorial-example

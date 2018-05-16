import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import darkBaseTheme from 'material-ui/styles/baseThemes/darkBaseTheme';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import store from './store';
import './index.css';
import App from './containers/App';
import registerServiceWorker from './registerServiceWorker';
import AppBar from 'material-ui/AppBar';
ReactDOM.render( 
    <Provider store={store}>
        <MuiThemeProvider muiTheme={getMuiTheme()}>
        <AppBar title="Recipe Manager by My Family Cooks" />   
        <App />
        </MuiThemeProvider>
    </Provider>, document.getElementById('root'));
registerServiceWorker();
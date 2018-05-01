import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import store from './store';
import './index.css';
import App from './containers/App';
import registerServiceWorker from './registerServiceWorker';

ReactDOM.render( 
    <Provider store={store}>
        <MuiThemeProvider muiTheme={getMuiTheme()}>
            <App />
        </MuiThemeProvider>
    </Provider>, document.getElementById('root'));
registerServiceWorker();
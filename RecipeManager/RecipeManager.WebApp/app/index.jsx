import React from 'react';
import { render } from 'react-dom';

import GoogleLogin from 'react-google-login';

class App extends React.Component {
    
    responseGoogle(response) {
        console.log(response);
    }
    render() {
        return (
            <div className="container body-content"> 
                <p>Hello React!</p>
                 <GoogleLogin
                    clientId="229269924386-ogl3ksdn474fn3bau6v6fr8ki221saa9.apps.googleusercontent.com"
                    buttonText="Login"
                    onSuccess={this.responseGoogle}
                    onFailure={this.responseGoogle}
                />
            </div> 
        );
    }
}

render(<App/>, document.getElementById('app'));
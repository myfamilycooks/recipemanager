import React from "react";
import { connect } from "react-redux";
import { Route, Router } from "react-router-dom";
import { DebugPage } from "../DebugPage";
import { HomePage } from "../HomePage";
import { LoginPage } from "../LoginPage";
import { RegisterPage } from "../RegisterPage";
import { alertActions } from "../_actions";
import { PrivateRoute } from "../_components";
import { history } from "../_helpers";
import "./App.css";

//https://github.com/cornflourblue/react-redux-registration-login-example

class App extends React.Component {
  constructor(props) {
    super(props);

    const { dispatch } = this.props;
    history.listen((location, action) => {
      dispatch(alertActions.clear());
    });
  }

  render() {
    return (
      <div className="jumbotron">
        <div className="container">
          <div className="col-sm-8 col-sm-offset-2">
            {alert.message && (
              <div className={`alert ${alert.type}`}>{alert.message}</div>
            )}
            <Router history={history}>
              <div>
                <PrivateRoute exact path="/" component={HomePage} />
                <Route path="/login" component={LoginPage} />
                <Route path="/register" component={RegisterPage} />
                <Route path="/debug" component={DebugPage} />
              </div>
            </Router>
          </div>
        </div>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const { alert } = state;
  return {
    alert
  };
};

const connectedApp = connect(mapStateToProps)(App);

export { connectedApp as App };

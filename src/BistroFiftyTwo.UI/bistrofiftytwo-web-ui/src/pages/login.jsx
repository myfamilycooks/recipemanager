import React from "react";
import LoginForm from "../components/loginForm";
const Public = () => <h3>Public</h3>;
const Protected = () => <h3>Protected</h3>;

class Login extends React.Component<*, *> {
  render() {
    return (
      <div className="row">
        <div className="col-sm-12" style={{ marginTop: "50px" }}>
          <div className="row">
            <div className="col-md-6 offset-md-3">
              <LoginForm />
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Login;

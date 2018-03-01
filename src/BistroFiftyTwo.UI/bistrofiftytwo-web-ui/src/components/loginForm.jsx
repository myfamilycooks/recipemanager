import React from "react";
import { Field, reduxForm } from "redux-form";

class LoginForm extends React.Component<*, *> {
  render() {
    return (
      <div className="card">
        <div className="card-header">
          <h3 className="mb-0">Login</h3>
        </div>
        <div className="card-body">
          <form className="form" role="form" autoComplete="off" id="formLogin">
            <div className="form-group">
              <label htmlFor="uname1">Username</label>
              <Field
                className="form-control"
                component="input"
                type="text"
                name="username"
              />
            </div>
            <div className="form-group">
              <label>Password</label>
              <Field
                className="form-control"
                component="input"
                type="password"
                name="password"
              />
            </div>

            <button type="button" className="btn btn-dark">
              Login
            </button>
          </form>
        </div>
      </div>
    );
  }
}

export default reduxForm({
  form: "login" // a unique identifier for this form
})(LoginForm);

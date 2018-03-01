import React from "react";

const Public = () => <h3>Public</h3>;
const Protected = () => <h3>Protected</h3>;

class Login extends React.Component<*, *> {
  render() {
    return <div>Login</div>;
  }
}

export default Login;

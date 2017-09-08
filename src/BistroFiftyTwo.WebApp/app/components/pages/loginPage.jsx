import React from 'react'
import Login from '../auth/login'

class LoginPage extends React.Component {
  submit = (values) => {
    // print the form values to the console
    const userid = values.userid;
    const password = values.password;
    this.props.loginUser({userid, password});
  }
  render() {
    return (
      <Login onSubmit={this.submit} />
    )
  }
}
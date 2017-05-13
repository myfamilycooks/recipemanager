import React from 'react';
import { reduxForm } from 'redux-form';
import * as actions from '../../actions';
import Header from '../header';

class Register extends React.Component {
  handleFormSubmit(formProps) {
    // call action creator to sign up the user!
  }


  renderAlert(){
    if(this.props.errorMessage) {
      return (
        <div className="alert alert-danger">
          <strong>Oops!</strong> {this.props.errorMessage}
        </div>
      )
    }
  }
  render() {
    const { handleSubmit, fields: { fullname, userid, password, passwordconfirm, email }} = this.props;
    return (
      <div>
        <Header />
        <h1>Register</h1>
        <form onSubmit={handleSubmit(this.handleFormSubmit.bind(this))}>
          <fieldset className="form-group">
            <label>Full Name</label>
            <input {...fullname} className="form-control"></input>
             {fullname.touched && fullname.error && <div className="error">{fullname.error}</div>}
          </fieldset>
          <fieldset className="form-group">
            <label>User ID</label>
            <input {...userid} className="form-control"></input>
            {userid.touched && userid.error && <div className="error">{userid.error}</div>}
   
          </fieldset>
          <fieldset className="form-group">
            <label>Password</label>
            <input {...password}  type="password" className="form-control"></input>
            {password.touched && password.error && <div className="error">{password.error}</div>}
          </fieldset>
          <fieldset className="form-group">
            <label>Confirm Password</label>
            <input {...passwordconfirm} type="password" className="form-control"></input>
            {passwordconfirm.touched && passwordconfirm.error && <div className="error">{passwordconfirm.error}</div>}
          </fieldset>
          <fieldset className="form-group">
            <label>Email</label>
            <input {...email} className="form-control"></input>            
            {email.touched && email.error && <div className="error">{email.error}</div>}
          </fieldset>
          {this.renderAlert()}
          <button action="submit" className="btn btn-primary">Log in</button>
        </form>
      </div>
    )
  }
}

function validate(formProps) {
  const errors = {};

  if(!formProps.fullname) {
    errors.fullname = "Please enter an fullname";
  }
  if(!formProps.userid) {
    errors.userid = "Please enter an userid";
  }
  if(!formProps.email) {
    errors.email = "Please enter an email";
  }

  if(!formProps.password) {
    errors.password = "Please enter an password";
  }

 if(!formProps.passwordconfirm) {
    errors.passwordconfirm = "Please enter an password confirmation";
  }

  if(formProps.password !== formProps.passwordconfirm) {
    errors.password = "Passwords must match.";
  }

  return errors;
}

function mapStateToProps(state) {
  return { errorMessage: state.auth.error };
}

export default reduxForm({
  form: 'register',
  fields: ['fullname','userid','password','passwordconfirm','email'],
  validate
}, mapStateToProps, actions)(Register);

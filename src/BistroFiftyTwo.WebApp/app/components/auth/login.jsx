import React from 'react';
import { connect } from 'react-redux';
import { Field, reduxForm, formValueSelector } from 'redux-form';
import { bindActionCreators } from 'redux'
import * as actionCreators from '../../actions';

class Login extends React.Component {

  handleFormSubmit({ userid, password }) {
    this.props.actions.loginUser({ userid, password });
  }
  renderAlert() {
    if (this.props.errorMessage) {
      return (
        <div className="alert alert-danger">
          <strong>Oops!</strong> {this.props.errorMessage}
        </div>
      )
    }
  }
  render() {
    const { handleSubmit } = this.props;
    return (
      <div className="row">
        <div className="col-md-6">
          <h2 className="margin-bottom-twenty">Log in to Recipe Manager</h2>
          <p className="margin-bottom-twenty">
            Or don't, maybe you want to refresh a bit and enjoy the beards.
            </p>
          <form onSubmit={handleSubmit((v) => this.handleFormSubmit(v))}>
            <fieldset className="form-group">
              <label>User ID</label>
              <Field name="userid" className="form-control" component="input" type="text" />
            </fieldset>
            <fieldset className="form-group">
              <label>Password</label>
              <Field name="password" className="form-control" component="input" type="password" />
            </fieldset>
            {this.renderAlert()}
            <button action="submit" className="btn btn-primary">Log in</button>
          </form>
        </div>
        <div className="col-md-4">
          <img src="http://placebeard.it/640/480"></img>
        </div>
      </div>
    )
  }
}
function mapDispatchToProps(dispatch) {
  return { actions: bindActionCreators(actionCreators, dispatch) }
}

Login = reduxForm({ form: 'login' })(Login);
Login = connect(
  state => ({
    initialValues: { userid: 'chef', password: 'mustard' },
    errorMessage: state.auth.error
  }), mapDispatchToProps)(Login);

export default Login;

import React from 'react';
import { reduxForm } from 'redux-form';
import * as actions from '../../actions';

class Login extends React.Component {

    handleFormSubmit({userid, password}) {
        this.props.loginUser({userid, password});
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
      const { handleSubmit, fields: { userid, password }} = this.props;
      return (
        <div className="row">
          <div className="col-md-6">
            <h2 className="margin-bottom-twenty">Log in to Recipe Manager</h2>
            <p className="margin-bottom-twenty">
              Or don't, maybe you want to refresh a bit and enjoy the beards.
            </p>
            <form onSubmit={handleSubmit(this.handleFormSubmit.bind(this))}>
                <fieldset className="form-group">
                  <label>User ID</label>
                  <input {...userid} className="form-control" />
                </fieldset>
                <fieldset className="form-group">
                  <label>Password</label>
                  <input  {...password} type="password" className="form-control" />
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

function mapStateToProps(state) {
  return { errorMessage: state.auth.error };
}

export default reduxForm({
  form: 'login',
  fields: ['userid','password']
}, mapStateToProps, actions)(Login);

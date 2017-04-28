import React from 'react';
import { connect } from 'react-redux';
import { CallbackComponent } from 'redux-oidc';
import { push } from 'react-router-redux';
import userManager from '../../utils/userManager';

class CallbackPage extends React.Component {
  constructor(props) {
    super(props)
    this.successCallback = this.successCallback.bind(this);
  }
  successCallback(){
    this.props.dispatch(push('/'));
  }

  render() {
    // just redirect to '/' in both cases
    return (
      <CallbackComponent userManager={userManager} 
                         successCallback={this.successCallback} 
                         errorCallback={this.successCallback}>
        <div>
          Redirecting...
        </div>
      </CallbackComponent>
    );
  }
}

function mapDispatchToProps(dispatch) {
  return {
    dispatch
  };
}

export default connect(null, mapDispatchToProps)(CallbackPage);
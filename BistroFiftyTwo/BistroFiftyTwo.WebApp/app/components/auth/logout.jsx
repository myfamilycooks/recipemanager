import React from 'react';
import { connect } from 'react-redux';
import * as actions from '../../actions';
import Header from '../header';

class LogOut extends React.Component {
  componentWillMount() {
    this.props.logOutUser();
  }

  render() {
    return (<div>
      <Header />
      <h1>Come Back Soon!</h1>
    </div>)
  }
}

export default connect(null, actions)(LogOut);

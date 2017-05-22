import React from 'react';
import { connect } from 'react-redux';
import * as actions from '../../actions';
import Header from '../header';

class Landing extends React.Component {
  componentWillMount() {
    const foo = this.props.loadProfile();
    console.log(foo);
  }

  render(){
    const user = this.props.profileUser ? this.props.profileUser : {};
     
    return(
      <div>
        <Header />
        <h2 className="margin-bottom-twenty">
          Welcome to the Recipe Portal
          {user.chef}
        </h2>
      </div>
    )
  }
}
function mapStateToProps(state) {
  return { profileUser: state.profile };
}

export default connect(mapStateToProps, actions)(Landing);

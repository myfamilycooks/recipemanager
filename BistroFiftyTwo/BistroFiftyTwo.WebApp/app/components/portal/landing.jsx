import React from 'react';
import { connect } from 'react-redux';
import * as actions from '../../actions';
import Header from '../header';

class Landing extends React.Component {
    componentDidMount() {
      this.setState({ isLoading: true });
      this.props.loadProfile();
      
    }

    render() {
        if (this.props.error) {
            console.log(this.props.error);
            return <p>Sorry! There was an error loading the items </p>;
        }
        if (this.props.isLoading) {
            return <p>Loading…</p>;
        }
        if(this.props.profile.user){
             return <span>Hello {this.props.profile.user.chef}</span>
            }
        return (
          <div>
       
            
            </div>
        );
    }
}
const mapStateToProps = (state) => {
  return {
    profile: state.profile,
    error: state.profileHasErrored,
    isLoading: state.profileIsLoading
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    loadProfile: () => dispatch(actions.loadProfile())
  }
}
export default connect(mapStateToProps, mapDispatchToProps)(Landing);
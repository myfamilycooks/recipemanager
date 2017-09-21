import React from 'react';
import { connect } from 'react-redux';
import { Switch, Route } from 'react-router-dom'
import * as actions from '../../actions';
import Header from '../header';
import Tasks from './tasks';

import RecipeEditor from '../pages/recipeEditor';
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
    if (this.props.profile.user) {
      console.log(this.props.match);
      return <div >

        <div className="container">
         
            <div className=" starter-template" style={{padding: "3rem 1.5rem"}}>
              <span>&nbsp;</span>
              <h1> <span>Hello {this.props.profile.user.chef}</span></h1>
                           
                <Route component={Tasks} />
              
            </div>
        
        </div>
    </div>
    }
    return (<div></div>);
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
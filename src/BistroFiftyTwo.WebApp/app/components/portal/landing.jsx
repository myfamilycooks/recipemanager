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
    if (this.props.profile.user) {
      return <div >
        <nav className="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
          <a className="navbar-brand" href="#">Recipe Manager</a>
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>

          <div className="collapse navbar-collapse" id="navbarsExampleDefault">
            <ul className="navbar-nav mr-auto">
              <li className="nav-item active">
                <a className="nav-link" href="#">Home <span className="sr-only">(current)</span></a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#">Link</a>
              </li>
              <li className="nav-item dropdown">
                <a className="nav-link dropdown-toggle" href="http://example.com" id="dropdown01" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dropdown</a>
                <div className="dropdown-menu" aria-labelledby="dropdown01">
                  <a className="dropdown-item" href="#">Action</a>
                  <a className="dropdown-item" href="#">Another action</a>
                  <a className="dropdown-item" href="#">Something else here</a>
                </div>
              </li>
            </ul>
          </div>
        </nav>
        <div className="container-fluid">
          <div className="row">
            <div className="col-md-2 col-lg-1 " style={{backgroundColor: "#f2f2f2", padding: "3rem"}}>
               <span>&nbsp;</span>
              <ul className="nav flex-column">
                <li className="nav-item">
                  <a className="nav-link active" href="#">My Recipes</a>
                </li>
                <li className="nav-item">
                  <a className="nav-link" href="#">New Recipe</a>
                </li>
              </ul>
            </div>
            <div className="col-md-9 starter-template" style={{padding: "3rem 1.5rem"}}>
              <span>&nbsp;</span>
              <h1> <span>Hello {this.props.profile.user.chef}</span></h1>
              <p className="lead">Use this document as a way to quickly start any new project.<br /> All you get is this text and a mostly barebones HTML document.</p>
            </div>
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
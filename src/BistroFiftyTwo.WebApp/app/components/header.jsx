import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router';

class Header extends React.Component {
    renderLoginOrLogout(){
      if(this.props.loggedIn)
        return (
          <ul className="nav navbar-nav">
          <li className="nav-item"><Link to="/logout" className="nav-link">Log Out</Link></li>
        </ul>
        )
      else {
        return (
        <ul className="nav navbar-nav">
          <li className="nav-item" key={1}><Link to="/login" className="nav-item">Log In</Link></li>
          <li className="nav-item" key={2}><Link to="/register" className="nav-item">Register</Link></li>
        </ul>
        );
      }
    }
    render(){
      return (
        <nav className="navbar navbar-inverse">
          <div className="navbar-header">
            <button type="button" className="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
              <span className="sr-only">Toggle navigation</span>
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
            </button>
            <Link to="/" className="navbar-brand">Recipe Manager</Link>
          </div>
          <ul className="nav navbar-nav">
            { this.renderLoginOrLogout()}
            <li className="nav-item"><a href="#">About</a></li>
          </ul>
        </nav>
      )
    }
}

function mapStateToProps(state) {
  return { loggedIn : state.auth.authenticated}
}

export default connect(mapStateToProps)(Header);

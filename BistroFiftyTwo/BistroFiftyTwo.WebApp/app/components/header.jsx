import React from 'react';

class Header extends React.Component {
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
            <a className="navbar-brand" href="#">Recipe Manager</a>
          </div>
          <ul className="nav navbar-nav">
            <li className="nav-item"><a href="#">Log In</a></li>
            <li className="nav-item"><a href="#">About</a></li>
          </ul>
        </nav>
      )
    }
}

export default Header;

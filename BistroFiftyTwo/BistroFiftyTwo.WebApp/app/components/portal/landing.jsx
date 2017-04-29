import React from 'react';
import Header from '../header';

class Landing extends React.Component {
  render(){
    return(
      <div>
        <Header />
        <h2 className="margin-bottom-twenty">
          Welcome to the Recipe Portal
        </h2>
      </div>
    )
  }
}

export default Landing;

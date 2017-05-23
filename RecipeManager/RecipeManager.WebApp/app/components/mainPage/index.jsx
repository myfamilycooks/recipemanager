import React from 'react';
import { connect } from 'react-redux';
import userManager from '../../utils/userManager';

class MainPage extends React.Component {
constructor(props) {
    super(props);
    this.showUserInfoButtonClick= this.showUserInfoButtonClick.bind(this);
}
  // display the current user
  showUserInfoButtonClick(event) {
    event.preventDefault();
    alert(JSON.stringify(this.props.user, null, 2));
  }

  // log out
  onLogoutButtonClicked(event) {
    event.preventDefault();
    userManager.removeUser(); // removes the user data from sessionStorage
  }



  render() {
    const { user, channels } = this.props;

    return (
      <div style={styles.root}>
        <div style={styles.title}>
          <h3>Welcome, {user ? user.profile.name : 'Mister Unknown'}!</h3>
         </div>
        <button onClick={this.showUserInfoButtonClick}>Show user info</button>
        <button onClick={this.onLogoutButtonClicked}>Logout</button>
      </div>
    );
  }
}

const styles = {
  root: {
    display: 'flex',
    flexDirection: 'column'
  },
  title: {
    flex: '1 0 auto',
  },
  list: {
    listStyle: 'none',
  },
  li: {
    display: 'flex',
  }
}

function mapStateToProps(state) {
  return {
    user: state.oidc.user,
  };
}

function mapDispatchToProps(dispatch) {
  return {
    dispatch
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(MainPage);
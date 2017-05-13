import axios from 'axios';
import querystring from 'querystring';
import { hashHistory } from 'react-router';
import { AUTH_USER, DEAUTH_USER, LOGIN_ERROR } from './types';

const ROOT_URL = ''; // we're using the same app, but that could change.

export function logOutUser() {
  localStorage.removeItem('token');
  return {
    type: DEAUTH_USER
  };
}

export function loginUser({userid, password}) {
  return function(dispatch) {
    // submit userid / password to server
    axios.post(`${ROOT_URL}/connect/token`, querystring.stringify({
      username: userid,
      password,
      client_id: 'client_id',
      grant_type: 'password',
      scope: 'openid offline_access'
    }), {
      headers: {
        "Content-Type": "application/x-www-form-urlencoded"
      }
    })
    .then(response => {
      console.log(response);
      // if request is good,
      // - update state to indicate user is authenticated
      dispatch({ type: AUTH_USER})
      // - save jwt token
      localStorage.setItem('token', JSON.stringify(response.data));
      // - redirect to user portal.
      hashHistory.push('/portal');
    })
    .catch(error => {
      // if request is bad
      // - show an error to user.
      dispatch(loginError('Could not verify your login info'));
    });
  }
}

export function loginError(error) {
  return {
    type: LOGIN_ERROR,
    payload: error
  };
}

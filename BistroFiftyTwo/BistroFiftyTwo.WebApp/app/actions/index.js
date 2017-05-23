import axios from 'axios';
import querystring from 'querystring';
import { hashHistory } from 'react-router';
import { AUTH_USER, DEAUTH_USER, LOGIN_ERROR, LOAD_PROFILE, PROFILE_HAS_ERRORED, PROFILE_IS_LOADING, PROFILE_HAS_LOADED } from './types';

const ROOT_URL = ''; // we're using the same app, but that could change.

export function logOutUser() {
    localStorage.removeItem('token');
    return {
        type: DEAUTH_USER
    };
}

export function loginUser({ userid, password }) {
    return function(dispatch) {
        // submit userid / password to server
        axios.post(`${ROOT_URL}/connect/token`, querystring.stringify({
                username: userid,
                password,
                grant_type: 'password',
            }), {
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            })
            .then(response => {
                console.log(response);
                // if request is good,
                // - update state to indicate user is authenticated
                dispatch({ type: AUTH_USER })
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

export function registerUser(user) {
    return function(dispatch) {
        axios.post(`${ROOT_URL}/api/register`, {
            username: user.userid,
            password,
            fullname,
            email
        });
    }
}
/*
export function loadProfile() {
    return function(dispatch) {
        const token = JSON.parse(localStorage.getItem('token'));
        axios.get(`${ROOT_URL}/api/profile/whoami`, { headers: { Authorization: `Bearer ${token.access_token}` } }).then(response => {
            dispatch({ type: LOAD_PROFILE, profile: response.data });
        }).catch(err => console.error(err));
    }
}
*/
export function profileHasErrored(bool, err) {
    return { 
        type: PROFILE_HAS_ERRORED, 
        hasErrored: bool,
        error: err
    };
}

export function profileIsLoading(bool) {
    return {
        type: PROFILE_IS_LOADING,
        isLoading: bool
    };
}

export function profileFetchedSuccessfully(profile) {
    return {
        type: PROFILE_HAS_LOADED,
        profile
    };
}

export function loadProfile() {
    return (dispatch) => {
        dispatch(profileIsLoading(true));

        const token = JSON.parse(localStorage.getItem('token'));
        axios.get(`${ROOT_URL}/api/profile/whoami`, { headers: { Authorization: `Bearer ${token.access_token}` } })
             .then(response => {
                  if(!response.status == 200) {
                      throw Error(response.statusText);
                  }
                  dispatch(profileIsLoading(false));
                  return response.data;
             })
             .then((profile) => dispatch(profileFetchedSuccessfully(profile)))
             .catch(err => dispatch(profileHasErrored(true, err)));
    }
}
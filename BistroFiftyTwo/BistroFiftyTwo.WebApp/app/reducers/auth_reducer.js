import { AUTH_USER, UNAUTH_USER, LOGIN_ERROR } from '../actions/types';

export default function(state = {}, action) {
  switch(action.type) {
    case AUTH_USER:
      return {...state, authenticated: true };
    case UNAUTH_USER:
      return {...state, authenticated: false };
    case LOGIN_ERROR:
      return {...state, error: action.payload };
  }

  return state;
}

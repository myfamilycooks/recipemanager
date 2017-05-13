import { AUTH_USER, DEAUTH_USER, LOGIN_ERROR } from '../actions/types';

export default function(state = {}, action) {
  switch(action.type) {
    case AUTH_USER:
      return {...state, authenticated: true };
    case DEAUTH_USER:
      return {...state, authenticated: false };
    case LOGIN_ERROR:
      return {...state, error: action.payload };
  }

  return state;
}

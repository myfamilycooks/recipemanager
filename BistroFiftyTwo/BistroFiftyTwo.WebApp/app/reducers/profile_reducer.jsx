import { LOAD_PROFILE } from '../actions/types';

export default function(state = { profile: null }, action) {
  switch(action.type) {
    case LOAD_PROFILE:
      return {...state, profile: action.profile };
  }

  return state;
}

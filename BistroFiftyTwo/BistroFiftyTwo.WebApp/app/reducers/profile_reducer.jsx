import { LOAD_PROFILE, PROFILE_HAS_ERRORED, PROFILE_IS_LOADING, PROFILE_HAS_LOADED } from '../actions/types';

export function profileHasErrored(state = false, action) {
    switch (action.type) {
        case PROFILE_HAS_ERRORED:
            return action.error;
        default:
            return state;
    }
}
export function profileIsLoading(state = false, action) {
    switch (action.type) {
        case PROFILE_IS_LOADING:
            return action.isLoading;
        default:
            return state;
    }
}
export function profile(state = [], action) {
    switch (action.type) {
        case PROFILE_HAS_LOADED:
            return action.profile;
        default:
            return state;
    }
}
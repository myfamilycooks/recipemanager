import { combineReducers } from 'redux';
import { reducer as form } from 'redux-form';
import authReducer from './auth_reducer';
import { profile, profileHasErrored, profileIsLoading} from './profile_reducer';

const rootReducer = combineReducers({
    form,
    auth: authReducer,
    profile,
    profileHasErrored,
    profileIsLoading
});

export default rootReducer;
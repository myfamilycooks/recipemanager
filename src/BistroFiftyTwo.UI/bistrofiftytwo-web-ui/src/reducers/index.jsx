import { createStore, combineReducers } from 'redux';
import { reducer as formReducer } from 'redux-form';
// Or with Immutablejs:
// import { reducer as formReducer } from 'redux-form/immutable';

const reducers = {
  // ... your other reducers here ...
  form: formReducer
};
const recipeManagerApp = combineReducers(reducers); 

export default recipeManagerApp;
//@flow
import { combineReducers } from "redux";
import { alert } from "./alert.reducer";
import { authentication } from "./authentication.reducer";
import { recipes } from "./recipe.reducer";
import { registration } from "./registration.reducer";
import { users } from "./users.reducer";

const rootReducer = combineReducers({
  authentication,
  registration,
  users,
  alert,
  recipes
});

export default rootReducer;

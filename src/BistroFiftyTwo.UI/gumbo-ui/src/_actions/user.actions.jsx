//@flow
import { userConstants } from "../_constants";
import { history } from "../_helpers";
import { userService } from "../_services";
import { alertActions } from "./";

export const userActions = {
  login,
  logout,
  register,
  getAll,
  delete: _delete
};

function login(username: String, password: String) {
  return dispatch => {
    dispatch(request({ username }));

    userService.login(username, password).then(
      user => {
        dispatch(success(user));
        history.push("/");
      },
      error => {
        dispatch(failure(error.toString()));
        dispatch(alertActions.error(error.toString()));
      }
    );
  };

  function request(user: Object) {
    return { type: userConstants.LOGIN_REQUEST, user };
  }
  function success(user: Object) {
    return { type: userConstants.LOGIN_SUCCESS, user };
  }
  function failure(error: Object) {
    return { type: userConstants.LOGIN_FAILURE, error };
  }
}

function logout() {
  userService.logout();
  return { type: userConstants.LOGOUT };
}

function register(user: Object) {
  return dispatch => {
    dispatch(request(user));

    userService.register(user).then(
      user => {
        dispatch(success());
        history.push("/login");
        dispatch(alertActions.success("Registration successful"));
      },
      error => {
        dispatch(failure(error.toString()));
        dispatch(alertActions.error(error.toString()));
      }
    );
  };

  function request(user: Object) {
    return { type: userConstants.REGISTER_REQUEST, user };
  }
  function success(user: Object) {
    return { type: userConstants.REGISTER_SUCCESS, user };
  }
  function failure(error: Object) {
    return { type: userConstants.REGISTER_FAILURE, error };
  }
}

function getAll() {
  return dispatch => {
    dispatch(request());

    userService
      .getAll()
      .then(
        users => dispatch(success(users)),
        error => dispatch(failure(error.toString()))
      );
  };

  function request() {
    return { type: userConstants.GETALL_REQUEST };
  }
  function success(users: Array<Object>) {
    return { type: userConstants.GETALL_SUCCESS, users };
  }
  function failure(error: Object) {
    return { type: userConstants.GETALL_FAILURE, error };
  }
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id: String) {
  return dispatch => {
    dispatch(request(id));

    userService
      .delete(id)
      .then(
        user => dispatch(success(id)),
        error => dispatch(failure(id, error.toString()))
      );
  };

  function request(id: String) {
    return { type: userConstants.DELETE_REQUEST, id };
  }
  function success(id: String) {
    return { type: userConstants.DELETE_SUCCESS, id };
  }
  function failure(id: String, error: Object) {
    return { type: userConstants.DELETE_FAILURE, id, error };
  }
}

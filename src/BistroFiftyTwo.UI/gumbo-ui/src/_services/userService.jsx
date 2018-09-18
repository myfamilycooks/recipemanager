//@flow
import axios from "axios";
import config from "../config";

export const userService = {
  login,
  logout,
  register,
  getAll,
  getById,
  update,
  delete: _delete
};

function login(username, password) {
  return axios
    .post(`${config.apiUrl}/token`, { username, password })
    .catch(handleError)
    .then(handleResponse)
    .then(user => {
      if (user.token) {
        localStorage.setItem(config.userItem, JSON.stringify(user));
      }

      return user;
    });
}

function logout() {
  localStorage.removeItem(config.userItem);
}

function getAll() {}

function getById(id) {}

function register(user) {}

function update(user) {}

function _delete(id) {}

function handleResponse(response) {
  if (response.status === 200) return response.data;
  else return Promise.reject(response.statusText);
}

function handleError(error) {
  if (error.response.status === 500) {
    // todo dispatch some 500 error page thingy-woo.
  } else {
    return error.response;
  }
}

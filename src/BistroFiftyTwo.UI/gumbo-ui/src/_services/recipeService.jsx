//@flow
import axios from "axios";
import config from "../config";
import { authHeader } from "../_helpers";

export const recipeService = {
  getFeatured,
  getByKey
};

function getByKey(key) {
  return axios.get(`${config.apiUrl}/api/`);
}

function getFeatured() {
  return axios
    .get(`${config.apiUrl}/api/featured`, { headers: authHeader() })
    .catch(handleError)
    .then(handleResponse)
    .then(recipes => recipes);
}

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

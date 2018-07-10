import axios from 'axios';
import { authHeader } from '../_helpers';

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
    axios.post()
}

function logout() {

}

function getAll() {

}

function getById(id) {

}

function register(user) {

}

function update(user) {

}

function _delete(id) {
    
}
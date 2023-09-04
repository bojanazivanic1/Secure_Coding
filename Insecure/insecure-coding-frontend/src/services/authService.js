import jwtDecode from "jwt-decode";
import api from "../api/api";
import { throwWarning } from "../helpers/helpers";

export const register = async (data) => {
    try {
        
console.log(data)
        await api.post("auth/register", data,
        {
            headers: { "Content-Type": "application/json" }
        });
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const login = async (data) => {
    try {
        const res = await api.post("auth/login", data,
        {
            headers: { "Content-Type": "application/json" }
        });
        localStorage.setItem('token', res.data);
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const resetPassword = async (data) => {
    try {
        await api.post("auth/reset-password", data,
        {
            headers: { "Content-Type": "application/json" }
        });
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const logout = () => {
    localStorage.removeItem("token");
};
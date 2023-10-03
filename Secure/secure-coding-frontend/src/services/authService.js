import api from "../api/api";
import { throwWarning } from "../helpers/helpers";

export const register = async (data) => {
    try {
        await api.post("auth/register", data,
        {
            headers: { "Content-Type": "application/json" }
        });
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const confirmEmail = async (data) => {
    try {
        const res = await api.post("auth/confirm-email", data,
        {
            headers: { "Content-Type": "application/json" }
        });
        return res.data;
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const confirmTotp = async (data) => {
    try {
        await api.post("auth/confirm-totp", data,
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
        await api.post("auth/login", data,
        {
            headers: { "Content-Type": "application/json" }
        });
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const confirmLogin = async (data) => {
    try {
        const res = await api.post("auth/confirm-login", data,
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

export const confirmPassword = async (data) => {
    try {
        await api.post("auth/confirm-password", data,
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
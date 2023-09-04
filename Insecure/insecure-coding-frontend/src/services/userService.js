import { toast } from "react-toastify";
import api from "../api/api";
import { throwWarning } from "../helpers/helpers";

export const addPost = async (data) => {
    try {
        await api.post("user/add-post", data,
        {
            headers: { "Content-Type": "application/json" }
        });
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const verifyPost = async (data) => {
    try {
        await api.post("user/verify-post", data,
        {
            headers: { "Content-Type": "application/json" }
        });
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const verifyModerator = async (data) => {
    try {
        await api.post("user/verify-moderator", data,
        {
            headers: { "Content-Type": "application/json" }
        });
    } catch (e) {
        if (e.response && e.response.status === 403) {
            toast.error("You are not authorized to perform this action.");
        }
        else {
            throwWarning(e);
        }
        return Promise.reject(e);
    }
};

export const getUnverifiedModerators = async (data) => {
    try {
        const res = await api.get("user/get-unverified-moderators", data);
        console.log(res.data)
        return res.data;
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const getVerifiedPosts = async (data) => {
    try {
        const res = await api.get("user/get-verified-posts", data);
        return res.data;
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const getAllPosts = async (data) => {
    try {
        const res = await api.get("user/get-all-posts", data);
        return res.data;
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};

export const deleteUser = async (data) => {
    try {  
        await api.delete("user/delete-user/" + data);
    } catch (e) {
        throwWarning(e);
        return Promise.reject(e);
    }
};
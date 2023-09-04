import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_LINK,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use((config) => {
  try {
    const token = localStorage.getItem("token");
    if (token) {
      return {
        ...config,
        headers: {
          ...config.headers,
          Authorization: `Bearer ${token}`,
        },
      };
    }
    return config;
  } catch (e) {
    return Promise.reject(e);
  }
});

export default api;
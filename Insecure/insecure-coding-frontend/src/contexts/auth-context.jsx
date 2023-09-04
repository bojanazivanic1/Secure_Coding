import { createContext, useEffect, useState } from "react";
import jwtDecode from "jwt-decode";

export const AuthContext = createContext({
    token: null,
    userType: ""
});

export const AuthContextProvider = ({ children }) => {
    const [token, setToken] = useState(null);
    const [enteredEmail, setEnteredEmail] = useState("");

    useEffect(() => {
        setToken(localStorage.getItem('token'));
    }, []);

    const userType = () => {
        try {
            if(!token)
                return null;
            const tokenDecoded = jwtDecode(token);
            return tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        } catch(e) {
            console.log(e);
        }
    };

    const temp = {
        token: token,
        type: userType,
    };

    return (
        <AuthContext.Provider value={temp}>
            { children }
        </AuthContext.Provider>
    );
}
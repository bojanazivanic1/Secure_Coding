import { createContext, useState } from "react";

export const QrContext = createContext({
    key: "",
    qr: "",
    email: "",
    setData: () => {}
});

export const QrContextProvider = ({ children }) => {
    const [response, setResponse] = useState({
        key: "",
        qr: "",
        email: ""
    });

    const setResponseHandler = (data) => {
        setResponse({ ...data });
    }

    const temp = {
        key: response.key,
        qr: response.qr,
        email: response.email,
        setData: setResponseHandler
    };

    return (
        <QrContext.Provider value={temp}>
            { children }
        </QrContext.Provider>
    );
};


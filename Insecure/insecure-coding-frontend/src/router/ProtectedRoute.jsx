import { Navigate } from "react-router-dom";

const ProtectedRoute = ( props ) => {

    const ret = () => {
        if(props.isLogged && localStorage.token) return props.component;
        if(!props.isLogged && !localStorage.token) return props.component;
        if(props.isLogged && !localStorage.token) return <Navigate to="/login" />;
        else return <Navigate to="/dashboard" />
    };

    return ret();
};

export default ProtectedRoute;
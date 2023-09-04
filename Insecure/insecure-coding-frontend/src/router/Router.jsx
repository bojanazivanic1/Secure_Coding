import React, { useContext, Suspense, lazy } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";

const Dashboard = lazy(() => import("../components/Dashboard/Dashboard"));
const Register = lazy(() => import("../components/Register/Register"));
const Login = lazy(() => import("../components/Login/Login"));
const ResetPassword = lazy(() => import("../components/ResetPassword/ResetPassword"));

const AppRouter = () => {
    return (
        <>
        <Suspense fallback={<div>Loading...</div>} >
            <Routes>
                <Route path="/" element={ <ProtectedRoute component={ <Login /> } /> } />   
                <Route path="/register" element={ <ProtectedRoute component={ <Register /> } /> } />   
                <Route path="/login" element={ <ProtectedRoute component={ <Login /> } /> } />   
                <Route path="/reset-password" element={ <ProtectedRoute component={ <ResetPassword /> } /> } />   
                <Route path="/dashboard" element={ <ProtectedRoute component={ <Dashboard /> } isLogged={ true } />} />
                <Route path="*" element={ <Navigate to="/" /> } />                
            </Routes>
        </Suspense>
        </>
    );
};

export default AppRouter;
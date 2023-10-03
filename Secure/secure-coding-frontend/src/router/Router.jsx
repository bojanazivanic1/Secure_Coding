import React, { useContext, Suspense, lazy } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";

const Dashboard = lazy(() => import("../components/Dashboard/Dashboard"));
const Register = lazy(() => import("../components/Register/Register"));
const ConfirmEmail = lazy(() => import("../components/Register/ConfirmEmail"));
const ConfirmTtop = lazy(() => import("../components/Register/ConfirmTotp"));
const Login = lazy(() => import("../components/Login/Login"));
const ConfirmLogin = lazy(() => import("../components/Login/ConfirmLogin"));
const ResetPassword = lazy(() => import("../components/ResetPassword/ResetPassword"));
const ConfirmPassword = lazy(() => import("../components/ResetPassword/ConfirmPassword"));

const AppRouter = () => {
    return (
        <>
        <Suspense fallback={<div>Loading...</div>} >
            <Routes>
                <Route path="/" element={ <ProtectedRoute component={ <Login /> } /> } />   
                <Route path="/register" element={ <ProtectedRoute component={ <Register /> } /> } />   
                <Route path="/confirm-email" element={ <ProtectedRoute component={ <ConfirmEmail /> } /> } />   
                <Route path="/confirm-totp" element={ <ProtectedRoute component={ <ConfirmTtop /> } /> } />   
                <Route path="/login" element={ <ProtectedRoute component={ <Login /> } /> } />   
                <Route path="/confirm-login" element={ <ProtectedRoute component={ <ConfirmLogin /> } /> } />   
                <Route path="/reset-password" element={ <ProtectedRoute component={ <ResetPassword /> } /> } />   
                <Route path="/confirm-password" element={ <ProtectedRoute component={ <ConfirmPassword /> } /> } />   
                <Route path="/dashboard" element={ <ProtectedRoute component={ <Dashboard /> } isLogged={ true } />} />
                <Route path="*" element={ <Navigate to="/" /> } />                
            </Routes>
        </Suspense>
        </>
    );
};

export default AppRouter;
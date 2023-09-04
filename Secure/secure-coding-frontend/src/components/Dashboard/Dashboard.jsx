import { lazy, useContext } from "react";
import { AuthContext } from "../../contexts/auth-context";

const AdminDashboard = lazy(() => import("../AdminDashboard/AdminDashboard"));
const ModeratorDashboard = lazy(() => import("../ModeratorDashboard/ModeratorDashboard"));
const ContributorDashboard = lazy(() => import("../ContributorDashboard/ContributorDashboard"));

const Dashboard = () => {
    const authContext = useContext(AuthContext);

    return (
        <>
            {authContext.type() === "ADMIN" && (
                <AdminDashboard />
            )}
            {authContext.type() === "MODERATOR" && (
                <ModeratorDashboard />
            )}
            {authContext.type() === "CONTRIBUTOR" && (
                <ContributorDashboard />
            )}
        </>
    );
};

export default Dashboard;
import { AppBar, Box, Button, Container, Toolbar, useTheme } from "@mui/material";
import { useContext } from "react";
import { AuthContext } from "../../contexts/auth-context";
import { logout } from "../../services/authService";
import { Link as RouterLink, useNavigate } from "react-router-dom";

const Navbar = () => {
    const authContext = useContext(AuthContext);
    const navigate = useNavigate();

    const logoutHandler = () => {
        logout();
        
        navigate("/login");

        window.location.reload();
    };

    return (
        <AppBar sx={{ backgroundColor: "grey" }}>
            <Container>
                <Toolbar>
                    <Box sx={{ flexGrow: 1 }} />
                    <Box>
                        {authContext.token ? (
                            <Button 
                                variant="contained"
                                onClick={logoutHandler}
                            >
                                Logout
                            </Button>
                        ) : (
                            <>
                                <Button
                                    variant="contained"
                                    component={RouterLink}
                                    to="/login"
                                >
                                    Login
                                </Button>
                                <Button
                                    sx={{ marginLeft: 2 }}
                                    variant="contained"
                                    component={RouterLink}
                                    to="/register"
                                >
                                    Register
                                </Button>
                            </>
                        )}
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default Navbar;
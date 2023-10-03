import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../../services/authService";
import LoginIcon from '@mui/icons-material/Login';
import { Button, Card, CardContent, TextField, Typography } from "@mui/material";
import { toast } from "react-toastify";

const Login = () => {
    const [data, setData] = useState({
        email: "",
        password: ""
    });
    const navigate = useNavigate();

    const changeHandler = (e) => {
        setData({ ...data, [e.target.name]:e.target.value });
    };

    const submitHandler = async (e) => {
        e.preventDefault();

        for (let key in data) {
            if(data[key] === "") {
                toast.warning("Please fill all fields!");
                return;
            }
        }

        await login(data)
            .then(() => {
                navigate("/confirm-login");
                sessionStorage.setItem("email", data.email);
            });
    };

    return (
        <>
        <Card component="form" >
            <CardContent>
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.email}
                    name="email"
                    type="email"
                    label="Email address"
                    onChange={changeHandler}
                />
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.password}
                    name="password"
                    type="password"
                    label="Password"
                    onChange={changeHandler}
                />
                <Button
                    variant="contained"
                    type="submit"
                    onClick={submitHandler}
                >
                    <LoginIcon />
                </Button>
            </CardContent>
        </Card>
        <Typography marginTop="20px">
        You forgot password?{' '}
            <Link to="/reset-password">
                Reset here.
            </Link>
        </Typography>
        </>
    );
};

export default Login;
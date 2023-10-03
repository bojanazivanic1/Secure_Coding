import { Button, Card, CardContent, CardMedia, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import VpnKeyIcon from '@mui/icons-material/VpnKey';
import { confirmPassword } from "../../services/authService";
import { toast } from "react-toastify";

const ConfirmPassword = () => {
    const [data, setData] = useState({
        code: "",
        password: "",
        confirmPassword: ""
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
        
        await confirmPassword({
            email: sessionStorage.getItem("email"),
            code: data.code,
            password: data.password,
            confirmPassword: data.confirmPassword
        })
            .then(() => {
                navigate("/login");
                sessionStorage.removeItem("email");
            });
    };

    return (
        <Card component="form">
            <CardContent>
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.code}
                    name="code" 
                    type="text"
                    label="TOTP Code"
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
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.confirmPassword}
                    name="confirmPassword"
                    type="password"
                    label="Confirm Password"
                    onChange={changeHandler}
                />
                <Button
                    variant="contained"
                    type="submit"
                    onClick={submitHandler}
                >
                    <VpnKeyIcon />
                </Button>
            </CardContent>
        </Card>
    );
};

export default ConfirmPassword;
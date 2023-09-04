import { Button, Card, CardContent, CardMedia, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import VpnKeyIcon from '@mui/icons-material/VpnKey';
import { resetPassword } from "../../services/authService";
import { toast } from "react-toastify";

const ResetPassword = () => {
    const [data, setData] = useState({
        email: "",
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
        
        await resetPassword({
            email: data.email,
            password: data.password,
            confirmPassword: data.confirmPassword
        })
            .then(() => navigate("/login"));
    };

    return (
        <>
        <Card sx={{ marginBottom: '80px', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
            <CardContent>
                <Typography variant="h6" sx={{ marginBottom: '10px' }}>
                    Scan the QR code using Google Authenticator
                </Typography>
                <CardMedia
                    component="img"
                    image={qrContext.qr}
                    alt="QR Code"
                    sx={{ marginBottom: '20px', maxWidth: '200px', margin: '0 auto' }}
                />
                <Typography variant="body2" sx={{ marginBottom: '10px' }}>
                    Or enter the key: <strong>{qrContext.key}</strong>
                </Typography>
            </CardContent>
        </Card>

        <Card component="form">
            <CardContent>
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.code}
                    name="code" 
                    type="text"
                    label="Code"
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
        </>
    );
};

export default ResetPassword;
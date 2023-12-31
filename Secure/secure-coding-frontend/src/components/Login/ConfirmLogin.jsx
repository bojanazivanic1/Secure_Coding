import { Button, Card, CardContent, CardMedia, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import VpnKeyIcon from '@mui/icons-material/VpnKey';
import { confirmLogin } from "../../services/authService";
import { toast } from "react-toastify";

const ConfirmLogin = () => {
    const [code, setCode] = useState("");
    const navigate = useNavigate();

    const changeHandler = (e) => {
        setCode(e.target.value);
    };

    const submitHandler = async (e) => {
        e.preventDefault();

        if (code === "") {
            toast.warning("Please fill the field!");
            return;
        }
        
        await confirmLogin({
            email: sessionStorage.getItem("email"),
            code: code
        })
            .then(() => {
                navigate("/dashboard");
                window.location.reload();
                sessionStorage.removeItem("email");
            });
    };

    return (
        <Card component="form">
            <CardContent>
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={code}
                    name="code" 
                    type="text"
                    label="TOTP Code"
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

export default ConfirmLogin;
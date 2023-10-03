import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { resetPassword } from "../../services/authService";
import { Button, Card, CardContent, TextField } from "@mui/material";
import LockResetIcon from '@mui/icons-material/LockReset';
import { toast } from "react-toastify";

const ResetPassword = () => {
    const [email, setEmail] = useState("");
    const navigate = useNavigate();

    const changeHandler = (e) => {
        setEmail(e.target.value);
    };

    const submitHandler = async (e) => {
        e.preventDefault();

        if(email === "") {
            toast.warning("Please fill the field!");
            return;
        }

        await resetPassword({
            email: email
        })
            .then(res => {
                navigate("/confirm-password");
                sessionStorage.setItem("email", email);
            });
    };

    return (
        <Card component="form">
            <CardContent>
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={email}
                    name="email"
                    type="email"
                    label="Email address"
                    onChange={changeHandler}
                />
                <Button
                    variant="contained"
                    type="submit"
                    onClick={submitHandler}
                >
                    <LockResetIcon />
                </Button>
            </CardContent>
        </Card>
    );
};

export default ResetPassword;
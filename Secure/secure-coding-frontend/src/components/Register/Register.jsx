import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../../services/authService"; 
import { Button, Card, Select, MenuItem, TextField, Link, Typography, CardContent, Checkbox, FormControlLabel, FormGroup } from "@mui/material";
import HowToReg from '@mui/icons-material/HowToReg';
import "react-toastify/dist/ReactToastify.css";
import { toast } from "react-toastify";

const Register = () => {
    const [data, setData] = useState({
        name: "",
        email: "",
        password: "",
        confirmPassword: "",
        userRole: 2,
    });
    const [isConsentChecked, setIsConsentChecked] = useState(false); // Dodatno stanje za praćenje čekboksa
    const navigate = useNavigate();

    const changeHandler = (e) => {
        setData({ ...data, [e.target.name]: e.target.value });
    };

    const consentHandler = (e) => {
        setIsConsentChecked(e.target.checked);
    };

    const submitHandler = async (e) => {
        e.preventDefault();

        for (let key in data) {
            if (data[key] === "") {
                toast.warning("Please fill all fields!");
                return;
            }
        }

        if (!isConsentChecked) {
            toast.warning("Please give consent to use your personal data!");
            return;
        }

        sessionStorage.setItem("email", data.email);

        await register(data).then(() => navigate("/confirm-email"));
    };

    return (
        <Card component="form">
            <CardContent>
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.name}
                    name="name"
                    type="text"
                    label="Name"
                    onChange={changeHandler}
                />
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
                <TextField 
                    required
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.confirmPassword}
                    name="confirmPassword"
                    type="password"
                    label="Confirm Password"
                    onChange={changeHandler}
                />
                <Select 
                    sx={{ marginBottom: "10px", width: "100%" }}
                    value={data.userRole}
                    name="userRole"
                    label="Role"
                    onChange={changeHandler}
                >
                    <MenuItem value={1}>Moderator</MenuItem>
                    <MenuItem value={2}>Contributor</MenuItem>
                </Select>
                <FormGroup row>
                    <FormControlLabel
                        control={
                            <Checkbox
                                checked={isConsentChecked}
                                onChange={consentHandler}
                            />
                        }
                        label="I agree to the use of my personal data for the purposes of this application."
                    />
                </FormGroup>
                <Button
                    variant="contained"
                    type="submit"
                    onClick={submitHandler}
                >
                    <HowToReg />
                </Button>
            </CardContent>
        </Card>
    );
};

export default Register;

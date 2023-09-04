import { Button, Card, CardContent, CardMedia, TextField, Typography } from "@mui/material";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import VpnKeyIcon from '@mui/icons-material/VpnKey';
import { confirmLogin } from "../../services/authService";
import { QrContext } from "../../contexts/qr-context";
import { toast } from "react-toastify";

const ConfirmLogin = () => {
    const [code, setCode] = useState("");
    const navigate = useNavigate();
    const qrContext = useContext(QrContext);

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
            email: qrContext.email,
            code: code
        })
            .then(() => {
                navigate("/dashboard");
                window.location.reload();
            });
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
                    value={code}
                    name="code" 
                    type="text"
                    label="Code"
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

export default ConfirmLogin;
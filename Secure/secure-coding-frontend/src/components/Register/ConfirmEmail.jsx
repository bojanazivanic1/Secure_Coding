import { Button, Card, CardContent, TextField } from "@mui/material";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import VpnKeyIcon from '@mui/icons-material/VpnKey';
import { confirmEmail } from "../../services/authService";
import { toast } from "react-toastify";
import { QrContext } from "../../contexts/qr-context";

const ConfirmEmail = () => {
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

        await confirmEmail({
            code: code,
            email: sessionStorage.getItem("email")
        })
            .then((res) => {
                qrContext.setData({ 
                    key: res.manualSetupKey,
                    qr: res.qrCodeImage,
                    email: sessionStorage.getItem("email")
                });
                navigate("/confirm-totp");
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
    );
};

export default ConfirmEmail;
import { Button, Card, CardContent, Typography } from "@mui/material";
import { verifyModerator } from "../../services/userService";
import CheckCircleRoundedIcon from '@mui/icons-material/CheckCircleRounded';
import { dateTimeToString, dateToString } from "../../helpers/helpers";

const Post = (props) => {
    
    const verifyHandler = async () => {
        await verifyModerator({
            id: props.id
        })
            .then(() => window.location.reload());
    }

    return (
        <Card sx={{ 
            display: 'flex', 
            flexDirection: 'column', 
            alignItems: 'stretch', 
            padding: 2, 
            boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)', 
            borderRadius: '8px', 
            overflow: 'hidden',
            backgroundColor: 'rgba(0, 123, 255, 0.1)', 
            height: 210,
        }}>
            <CardContent>
                <Typography variant="h5" color="primary">
                    {props.name}
                </Typography>
            </CardContent>
            <Card sx={{ border: '1px solid #007BFF', borderRadius: '8px', overflow: 'hidden', backgroundColor: 'white',  height: "200" }}>
                <CardContent>
                    <Typography>{props.email}</Typography>
                </CardContent>
            </Card>
            {!props.verify && (
                <Button 
                    variant="contained" 
                    color="primary" 
                    onClick={verifyHandler}
                    sx={{ borderRadius: '0 0 8px 8px', borderTop: '1px solid #007BFF' }} 
                >
                    verify
                    <CheckCircleRoundedIcon />
                </Button>
            )}
        </Card>
        
    );
};

export default Post;
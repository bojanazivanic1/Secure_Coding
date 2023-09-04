import { Button, Card, CardContent, Typography, Box } from "@mui/material";
import { verifyPost } from "../../services/userService";
import CheckCircleRoundedIcon from '@mui/icons-material/CheckCircleRounded';

const Post = (props) => {

    const verifyHandler = async () => {
        await verifyPost({
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
        }}>
            <CardContent>
                <Typography variant="h5" color="primary">
                    {props.contributorId}
                </Typography>
            </CardContent>
            <Box sx={{ flexGrow: 1 }}>
                <Card sx={{ padding: 1, border: '1px solid #007BFF', borderRadius: '8px', overflow: 'hidden', backgroundColor: 'white' }}>
                    <CardContent>
                        <Typography>{props.message}</Typography>
                    </CardContent>
                </Card>
            </Box>
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

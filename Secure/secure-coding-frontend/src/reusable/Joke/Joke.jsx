import { Card, CardContent, Typography } from "@mui/material"

const Joke = ({ jokeData }) => {
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
                <Typography>{jokeData}</Typography> 
            </CardContent>
        </Card>
    );
};

export default Joke;
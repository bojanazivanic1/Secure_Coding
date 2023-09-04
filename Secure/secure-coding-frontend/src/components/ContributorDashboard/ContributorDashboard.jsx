import { Button, Card, CardContent, Container, Grid, TextField, Typography } from "@mui/material";
import React, { lazy, useEffect, useState } from "react";
import PostAddIcon from '@mui/icons-material/PostAdd';
import { addPost, getVerifiedPosts } from "../../services/userService";
import { toast } from "react-toastify";

const Post = lazy(() => import("../../reusable/Post/Post"));

const ContributorDashboard = () => {
    const [message, setMessage] = useState("");
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        getVerifiedPosts()
            .then((res) => {
                if(res != null) {
                    setPosts(res.map(post => 
                    <Post 
                        key={post.id}
                        id={post.id}
                        contributorId={post.contributorId}
                        message={post.message}
                        verify={true}
                    />)
                );
            }
        })
    }, []);

    const messageChangeHandler = (e) => {
        setMessage(e.target.value);
    }

    const submitHandler = async (e) => {
        e.preventDefault();

        if(message === "") {
            toast.warning("Please fill the field!");
            return;
        }

        await addPost({
            message: message
        });

        window.location.reload();
    }; 

    return (
        <Container>
    <Grid container sx={{ marginTop: 10 }} spacing={2}>
        <Grid item xs={12} md={16}>
            <Card component="form">
                <CardContent sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center' }}>
                    <Typography variant="h6">Add Post</Typography>
                    <TextField 
                        required
                        sx={{ marginBottom: "10px", width: "100%" }}
                        value={message}
                        name="message"
                        type="text"
                        label="Message"
                        multiline
                        rows={4}
                        onChange={messageChangeHandler}
                    />
                    <Button
                        variant="contained"
                        type="submit"
                        onClick={submitHandler}
                    >
                        <PostAddIcon />
                    </Button>
                </CardContent>
            </Card>
        </Grid>

        <Grid container spacing={2} style={{ marginTop: '50px' }}>
            <Grid item xs={12} sx={{ textAlign: 'center' }}> 
                <Typography variant="h4">Posts</Typography>
            </Grid>
            {posts.map((post, index) => (
                <Grid key={index} item xs={12}>
                    {post}
                </Grid>
            ))}
        </Grid>
    </Grid>
</Container>

    );
};

export default ContributorDashboard;
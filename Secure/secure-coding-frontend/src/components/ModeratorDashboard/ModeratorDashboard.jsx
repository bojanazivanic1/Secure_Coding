import { Container, Grid, Typography } from "@mui/material";
import { getAllPosts } from "../../services/userService";
import { lazy, useEffect, useState } from "react";

const Post = lazy(() => import("../../reusable/Post/Post"));

const ModeratorDashboard = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        getAllPosts()
            .then((res) => {
                if(res != null) {
                    setPosts(res.map(post =>
                        <Post 
                            key={post.id}
                            id={post.id}
                            contributorId={post.contributorId}
                            message={post.message}
                            verify={post.messageVerified}
                        />)
                );
            }
        })
    }, []);

    return (
        <Container>
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
        </Container>

    );
};

export default ModeratorDashboard;
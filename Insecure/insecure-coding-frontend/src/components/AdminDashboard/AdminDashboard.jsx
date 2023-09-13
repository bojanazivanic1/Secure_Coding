import React, { useEffect, useState } from "react";
import { Button, Card, CardContent, Container, Grid, TextField, Typography, Box } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import { deleteUser, getAllPosts, getUnverifiedModerators } from "../../services/userService";
import { toast } from "react-toastify";
import Post from "../../reusable/Post/Post";
import Moderator from "../../reusable/Moderator/Moderator";

const AdminDashboard = () => {
    const [posts, setPosts] = useState([]);
    const [moderators, setModerators] = useState([]);
    const [id, setId] = useState(0);

    useEffect(() => {
        getAllPosts()
            .then((res) => {
                if (res != null) {
                    setPosts(res.map((post) => (
                        <Grid key={post.id} item xs={12}>
                            <Post
                                id={post.id}
                                contributorId={post.contributorId}
                                message={post.message}
                                verify={post.messageVerified}
                            />
                            <Box sx={{ marginBottom: 2 }}></Box>
                        </Grid>
                    )));
                }
            });

        getUnverifiedModerators()
            .then((res) => {
                if (res != null) {
                    console.log(res)
                    setModerators(res.map((mod) => (
                        <Grid key={mod.id} item xs={12}>
                            <Moderator
                                id={mod.id}
                                name={mod.name}
                                email={mod.email}
                                verify={false}
                            />
                            <Box sx={{ marginBottom: 2 }}></Box>
                        </Grid>
                    )));
                }
            });
    }, []);

    const idChangeHandler = (e) => {
        setId(e.target.value);
    };

    const submitHandler = async (e) => {
        e.preventDefault();

        if (id === 0) {
            toast.warning("Please enter the id.");
        }

        await deleteUser(id)
            .then(() => {
                setId(0);
                toast.success("Deleted successfully!");
            });
    };

    return (
        <Container maxWidth={false}>
            <Grid container spacing={4}>
                <Grid item xs={12} md={8} sx={{ marginTop: 10 }}>
                    <Typography variant="h4">Posts</Typography>
                    {posts}
                </Grid>

                <Grid item xs={12} md={4} sx={{ marginTop: 10 }}>
                    <Typography variant="h4">Moderators</Typography>
                    <Grid container spacing={2}>
                        {moderators}
                    </Grid>
                </Grid>

                <Grid item xs={12} sx={{ marginTop: 3 }}>
                    <Card component="form" sx={{ alignItems: 'center', padding: 2, boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)', borderRadius: '8px', overflow: 'hidden', backgroundColor: 'rgba(0, 123, 255, 0.1)' }}>
                        <CardContent>
                            <Typography variant="h4">Delete User</Typography>
                            <TextField
                                sx={{ marginBottom: "20px", width: "100%" }}
                                value={id}
                                name="Id"
                                type="number"
                                label="Id"
                                onChange={idChangeHandler}
                            />
                            <Button
                                variant="contained"
                                color="primary"
                                type="submit"
                                onClick={submitHandler}
                                sx={{ borderRadius: '0 0 8px 8px', borderTop: '1px solid #007BFF' }}
                            >
                                <DeleteIcon />
                            </Button>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </Container>
    );
};

export default AdminDashboard;

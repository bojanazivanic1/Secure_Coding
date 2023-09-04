using InsecureCode.Interfaces.IProviders;
using InsecureCode.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace InsecureCode.Infrastructure.Providers
{
    public class PostDbProvider : IPostDbProvider
    {
        static IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        string connectionString = configuration.GetConnectionString("AppDbContext");

        public async Task<bool> AddPostAsync(Post newPost)
        {
            string query = "INSERT INTO Posts (Message, MessageVerified, ContributorId) VALUES ('" + newPost.Message + "', 'False', " + newPost.ContributorId + ")";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandText = query;
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<Post>> GetAllPostsAsync(int contributorId)
        {
            string query = "SELECT * FROM Posts WHERE ContributorId = '" + contributorId + "'";
            List<Post> posts = new List<Post>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandText = query;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Post post = new Post
                            {
                                Id = reader.GetInt32(0),
                                Message = reader.GetString(1),
                                MessageVerified = reader.GetBoolean(2),
                                ContributorId = reader.GetInt32(3)
                            };
                            posts.Add(post);
                        }
                    }
                }
            }

            return posts;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            string query = "SELECT * FROM Posts";
            List<Post> posts = new List<Post>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    command.CommandText = query;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Post post = new Post
                            {
                                Id = reader.GetInt32(0),
                                Message = reader.GetString(1),
                                MessageVerified = reader.GetBoolean(2),
                                ContributorId = reader.GetInt32(3)
                            };
                            posts.Add(post);
                        }
                    }
                }
            }

            return posts;
        }

        public async Task<bool> VerifyPostAsync(int postId)
        {
            string query = "UPDATE Posts SET MessageVerified = 1 WHERE Id = " + postId;

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandText = query;
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<Post>> GetVerifiedPostsAsync()
        {
            string query = "SELECT * FROM Posts WHERE MessageVerified = 1";
            List<Post> posts = new List<Post>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Post post = new Post
                            {
                                Id = reader.GetInt32(0),
                                Message = reader.GetString(1),
                                MessageVerified = reader.GetBoolean(2),
                                ContributorId = reader.GetInt32(3)
                            };
                            posts.Add(post);
                        }
                    }
                }
            }

            return posts;
        }

        public async Task<Post?> FindPostByIdAsync(int id)
        {
            string query = "SELECT * FROM Posts WHERE Id = " + id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.Read())
                            return null;

                        Post post = new Post
                        {
                            Id = (int)reader["Id"],
                            Message = (string)reader["Message"],
                            MessageVerified = (bool)reader["MessageVerified"],
                            ContributorId = (int)reader["ContributorId"]
                        };

                        return post;
                    }
                }
            }
        }
    }
}

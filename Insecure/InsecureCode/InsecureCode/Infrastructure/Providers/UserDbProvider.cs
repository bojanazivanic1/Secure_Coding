using InsecureCode.Interfaces.IProviders;
using InsecureCode.Models;
using System.Data;
using System.Data.SqlClient;

namespace InsecureCode.Infrastructure.Providers
{
    public class UserDbProvider : IUserDbProvider
    {
        static IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        string connectionString = configuration.GetConnectionString("AppDbContext");

        public async Task<bool> AddUserAsync(User newUser)
        {
            string query = "INSERT INTO Users (Name, Email, Password, UserRole, ModeratorVerifiedAt) VALUES (" +
               "'" + newUser.Name + "', " +
               "'" + newUser.Email + "', " +
               "'" + newUser.Password + "', " +
               "'" + newUser.UserRole.ToString() + "', " +
               (newUser.ModeratorVerifiedAt != null 
               ? "'" + newUser.ModeratorVerifiedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" 
               : "NULL") + ")";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }          
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            string query = "UPDATE Users SET " +
               "Name = '" + user.Name + "', " +
               "Email = '" + user.Email + "', " +
               "Password = '" + user.Password + "', " +
               "UserRole = '" + user.UserRole + "', " +
               "ModeratorVerifiedAt = " + (user.ModeratorVerifiedAt != null 
                                        ? "'" + user.ModeratorVerifiedAt.ToString() + "'"
                                        : "NULL") + " " +
               "WHERE Id = " + user.Id;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<User?> FindUserByEmailAsync(string email)
        {
            string query = "SELECT * FROM Users WHERE Email = '" + email + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.Read())
                            return null;
                        User user = new User
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            Password = (string)reader["Password"],
                            UserRole = (EUserRole)Enum.Parse(typeof(EUserRole), (string)reader["UserRole"]),
                            ModeratorVerifiedAt = reader["ModeratorVerifiedAt"] != DBNull.Value 
                                                ? (DateTime)reader["ModeratorVerifiedAt"] 
                                                : null
                        };
                        return user;
                    }
                }
            }
        }

        public async Task<User?> FindUserByIdAsync(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = '" + id + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.Read())
                            return null;

                        User user = new User
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            Password = (string)reader["Password"],
                            UserRole = (EUserRole)Enum.Parse(typeof(EUserRole), (string)reader["UserRole"]),
                            ModeratorVerifiedAt = reader["ModeratorVerifiedAt"] != DBNull.Value ? (DateTime)reader["ModeratorVerifiedAt"] : null
                        };

                        return user;
                    }
                }
            }
        }

        public async Task<bool> VerifyModeratorAsync(int userId)
        {
            string query = "UPDATE Users SET ModeratorVerifiedAt = GETDATE() WHERE Id = '" + userId + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<User>> GetUnverifiedModeratorsAsync()
        {
            string query = "SELECT * FROM Users WHERE UserRole = 'MODERATOR' AND ModeratorVerifiedAt IS NULL";

            List<User> moderators = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            User moderator = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(3),
                                Password = reader.GetString(2),
                                UserRole = (EUserRole)Enum.Parse(typeof(EUserRole), (string)reader["UserRole"]),
                                ModeratorVerifiedAt = reader["ModeratorVerifiedAt"] != DBNull.Value ? (DateTime)reader["ModeratorVerifiedAt"] : null
                            };
                            moderators.Add(moderator);
                        }
                    }
                }
            }

            return moderators;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            string query = "DELETE FROM Users WHERE Id = '" + userId + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}

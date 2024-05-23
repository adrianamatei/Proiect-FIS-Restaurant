using Proiect_FIS_Restaurant.Proiect_FIS_Restaurant;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Proiect_FIS_Restaurant
{
    public class UserManager
    {
        private Database _database;

        public UserManager(Database database)
        {
            _database = database;
        }

        public void AddUser(User user)
        {
            using (var connection = _database.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)", connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Role", user.Role);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public User GetUser(string username)
        {
            using (var connection = _database.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserId = (int)reader["UserId"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = (string)reader["Role"]
                        };
                    }
                }
            }
            return null;
        }

        public List<User> GetUsers()
        {
            var users = new List<User>();
            using (var connection = _database.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Users", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = (int)reader["UserId"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = (string)reader["Role"]
                        });
                    }
                }
            }
            return users;
        }

        public User Authenticate(string username, string password)
        {
            try
            {
                using (var connection = _database.GetConnection())
                {
                    var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = (int)reader["UserId"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Role = (string)reader["Role"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception for debugging purposes
                Console.WriteLine($"Exception during authentication: {ex.Message}");
                throw;
            }
            return null;
        }
    }
}

using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace Proiect_FIS_Restaurant
{
    public class Database
    {
        private readonly string connectionString;

        public Database()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;
        }
        // Constructor suplimentar pentru teste
        public Database(string testConnectionString)
        {
            connectionString = testConnectionString;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public virtual List<MenuItem> GetMenuItems()
        {
            var menuItems = new List<MenuItem>();
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT MenuItemId, Name, Category, Price, Ingredients, IsSpicy, IsVegetarian, IsAvailable FROM MenuItems";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            menuItems.Add(new MenuItem
                            {
                                MenuItemId = reader.GetInt32(reader.GetOrdinal("MenuItemId")),
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Ingredients = reader["Ingredients"].ToString(),
                                IsSpicy = reader.GetBoolean(reader.GetOrdinal("IsSpicy")),
                                IsVegetarian = reader.GetBoolean(reader.GetOrdinal("IsVegetarian")),
                                IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
                            });
                        }
                    }
                }
            }
            return menuItems;
        }

        public virtual MenuItem GetMenuItemById(int menuItemId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT MenuItemId, Name, Category, Price, Ingredients, IsSpicy, IsVegetarian, IsAvailable FROM MenuItems WHERE MenuItemId = @MenuItemId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MenuItemId", menuItemId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MenuItem
                            {
                                MenuItemId = reader.GetInt32(reader.GetOrdinal("MenuItemId")),
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Ingredients = reader["Ingredients"].ToString(),
                                IsSpicy = reader.GetBoolean(reader.GetOrdinal("IsSpicy")),
                                IsVegetarian = reader.GetBoolean(reader.GetOrdinal("IsVegetarian")),
                                IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public virtual void AddMenuItem(MenuItem menuItem)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO MenuItems (Name, Category, Price, Ingredients, IsSpicy, IsVegetarian, IsAvailable) VALUES (@Name, @Category, @Price, @Ingredients, @IsSpicy, @IsVegetarian, @IsAvailable)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", menuItem.Name);
                    command.Parameters.AddWithValue("@Category", menuItem.Category);
                    command.Parameters.AddWithValue("@Price", menuItem.Price);
                    command.Parameters.AddWithValue("@Ingredients", menuItem.Ingredients);
                    command.Parameters.AddWithValue("@IsSpicy", menuItem.IsSpicy);
                    command.Parameters.AddWithValue("@IsVegetarian", menuItem.IsVegetarian);
                    command.Parameters.AddWithValue("@IsAvailable", menuItem.IsAvailable);

                    command.ExecuteNonQuery();
                }
            }
        }

        public virtual void UpdateMenuItemAvailability(string itemName, bool isAvailable)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE MenuItems SET IsAvailable = @IsAvailable WHERE Name = @Name";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", itemName);
                    command.Parameters.AddWithValue("@IsAvailable", isAvailable);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

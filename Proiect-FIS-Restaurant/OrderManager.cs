using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Proiect_FIS_Restaurant
{
    public class OrderManager
    {
        private Database _database;

        public OrderManager(Database database)
        {
            _database = database;
        }

        public void AddOrder(Order order)
        {
            using (var connection = _database.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Orders (OrderTime, EstimatedTime, Status) OUTPUT INSERTED.OrderId VALUES (@OrderTime, @EstimatedTime, @Status)", connection);
                command.Parameters.AddWithValue("@OrderTime", order.OrderTime);
                command.Parameters.AddWithValue("@EstimatedTime", order.EstimatedTime);
                command.Parameters.AddWithValue("@Status", order.Status);

                connection.Open();
                order.OrderId = (int)command.ExecuteScalar();

                foreach (var item in order.Items)
                {
                    var orderDetailCommand = new SqlCommand("INSERT INTO OrderDetails (OrderId, MenuItemId, Quantity) VALUES (@OrderId, @MenuItemId, @Quantity)", connection);
                    orderDetailCommand.Parameters.AddWithValue("@OrderId", order.OrderId);
                    orderDetailCommand.Parameters.AddWithValue("@MenuItemId", item.MenuItemId);
                    orderDetailCommand.Parameters.AddWithValue("@Quantity", 1); // Assuming 1 for simplicity
                    orderDetailCommand.ExecuteNonQuery();
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();

            using (var connection = _database.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Orders", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order
                        {
                            OrderId = (int)reader["OrderId"],
                            OrderTime = (DateTime)reader["OrderTime"],
                            EstimatedTime = (DateTime)reader["EstimatedTime"],
                            Status = (string)reader["Status"],
                            Items = new List<OrderItem>()
                        };

                        orders.Add(order);
                    }
                }

                // Fetch order items separately after closing the initial reader
                foreach (var order in orders)
                {
                    var orderDetailsCommand = new SqlCommand("SELECT * FROM OrderDetails JOIN MenuItems ON OrderDetails.MenuItemId = MenuItems.MenuItemId WHERE OrderId = @OrderId", connection);
                    orderDetailsCommand.Parameters.AddWithValue("@OrderId", order.OrderId);

                    using (var orderDetailsReader = orderDetailsCommand.ExecuteReader())
                    {
                        while (orderDetailsReader.Read())
                        {
                            order.Items.Add(new OrderItem
                            {
                                MenuItemId = (int)orderDetailsReader["MenuItemId"],
                                Name = (string)orderDetailsReader["Name"],
                                Status = "Ordered"
                            });
                        }
                    }
                }
            }

            return orders;
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            using (var connection = _database.GetConnection())
            {
                var command = new SqlCommand("UPDATE Orders SET Status = @Status WHERE OrderId = @OrderId", connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@OrderId", orderId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void ConfirmOrderPayment(int orderId, int receiptNumber)
        {
            using (var connection = _database.GetConnection())
            {
                var command = new SqlCommand("UPDATE Orders SET Status = 'Servită', ReceiptNumber = @ReceiptNumber WHERE OrderId = @OrderId", connection);
                command.Parameters.AddWithValue("@ReceiptNumber", receiptNumber);
                command.Parameters.AddWithValue("@OrderId", orderId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

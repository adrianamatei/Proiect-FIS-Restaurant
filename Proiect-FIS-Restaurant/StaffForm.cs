using System;
using System.Linq;
using System.Windows.Forms;

namespace Proiect_FIS_Restaurant
{
    public partial class StaffForm : Form
    {
        private OrderManager _orderManager;
        private MenuManager _menuManager;
        private Database _database;

        public StaffForm(Database database)
        {
            InitializeComponent();
            _orderManager = new OrderManager(database);
            _menuManager = new MenuManager(database);
            LoadAllOrders();
        }

        private void LoadAllOrders()
        {
            listViewOrders.Columns.Clear();
            listViewOrders.Items.Clear();
            listViewOrders.Columns.Add("Order ID", 100);
            listViewOrders.Columns.Add("Status", 100);
            listViewOrders.Columns.Add("Estimated Time", 150);
            listViewOrders.Columns.Add("Items", 300);

            var orders = _orderManager.GetAllOrders();
            foreach (var order in orders)
            {
                string itemsDetails = string.Join(", ", order.Items.Select(i => $"{i.Name} ({i.Status})"));
                var listViewItem = new ListViewItem(new[]
                {
                    order.OrderId.ToString(),
                    order.Status,
                    order.EstimatedTime.ToString(),
                    itemsDetails
                });
                listViewOrders.Items.Add(listViewItem);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void payment_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedOrder = listViewOrders.SelectedItems[0];
                var orderId = int.Parse(selectedOrder.SubItems[0].Text);
                var receiptNumber = int.Parse(txtReceiptNumber.Text);
                _orderManager.ConfirmOrderPayment(orderId, receiptNumber);
                listViewOrders.Items.Remove(selectedOrder);
                MessageBox.Show("Payment confirmed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedOrder = listViewOrders.SelectedItems[0];
                var orderId = int.Parse(selectedOrder.SubItems[0].Text);
                var status = cmbOrderStatus.SelectedItem.ToString();
                _orderManager.UpdateOrderStatus(orderId, status);
                selectedOrder.SubItems[1].Text = status;
                MessageBox.Show("Order status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

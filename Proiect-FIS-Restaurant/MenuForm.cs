using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Proiect_FIS_Restaurant
{
    public partial class MenuForm : Form
    {
        private MenuManager _menuManager;
        private List<MenuItem> _cart;
        private OrderManager _orderManager;

        public MenuForm(Database database)
        {
            InitializeComponent();
            _menuManager = new MenuManager(database);
            _orderManager = new OrderManager(database);
            _cart = new List<MenuItem>();
            LoadMenuItems();
        }

        public MenuForm()
        {
            InitializeComponent();
            this.Size = new Size(800, 600); // Set form size
            this.MinimumSize = new Size(800, 600); // Set minimum form size
        }

        private void LoadMenuItems()
        {
            var menuItems = _menuManager.GetMenuItems();
            listViewMenu.Items.Clear();
            foreach (var item in menuItems)
            {
                var listItem = new ListViewItem(new[] {
                    item.Name,
                    item.Category,
                    item.Price.ToString("C"),
                    item.Ingredients,
                    item.IsSpicy ? "Yes" : "No",
                    item.IsVegetarian ? "Yes" : "No",
                    item.IsAvailable ? "Yes" : "No"
                });
                listViewMenu.Items.Add(listItem);
            }
        }

        private void btnWelcome_Click(object sender, EventArgs e)
        {
            Welcome welcomeForm = new Welcome(); // Create an instance of the WelcomeForm
            welcomeForm.Show(); // Show the WelcomeForm
            this.Hide(); // Hide the current form
        }

        private void Cart_Click(object sender, EventArgs e)
        {
            if (listViewMenu.SelectedItems.Count > 0)
            {
                var selectedItem = listViewMenu.SelectedItems[0];
                var menuItem = _menuManager.GetMenuItems().FirstOrDefault(m => m.Name == selectedItem.SubItems[0].Text);
                if (menuItem != null)
                {
                    if (menuItem.IsAvailable)
                    {
                        _cart.Add(menuItem); // Change `add` to `Add`
                        MessageBox.Show($"{menuItem.Name} added to cart.");
                    }
                    else
                    {
                        MessageBox.Show($"{menuItem.Name} is not available.");
                    }
                }
            }
        }

        private void details_Click(object sender, EventArgs e)
        {
            if (listViewMenu.SelectedItems.Count > 0)
            {
                var selectedItem = listViewMenu.SelectedItems[0];
                var menuItem = _menuManager.GetMenuItems().FirstOrDefault(m => m.Name == selectedItem.SubItems[0].Text);
                if (menuItem != null)
                {
                    var detailsForm = new DetailsForm(menuItem);
                    detailsForm.ShowDialog();
                }
            }
        }

        private void order_Click(object sender, EventArgs e)
        {
            if (_cart.Any())
            {
                var order = new Order
                {
                    Items = _cart.Select(m => new OrderItem
                    {
                        MenuItemId = m.MenuItemId,
                        Name = m.Name,
                        Status = "Ordered"
                    }).ToList(),
                    OrderTime = DateTime.Now,
                    EstimatedTime = DateTime.Now.AddMinutes(30),
                    Status = "În așteptare"
                };
                _orderManager.AddOrder(order);
                _cart.Clear();
                MessageBox.Show("Order placed successfully!");
            }
            else
            {
                MessageBox.Show("Cart is empty!");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

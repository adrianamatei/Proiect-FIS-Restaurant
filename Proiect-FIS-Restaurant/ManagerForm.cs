using Proiect_FIS_Restaurant.Proiect_FIS_Restaurant;
using System;
using System.Windows.Forms;

namespace Proiect_FIS_Restaurant
{
    public partial class ManagerForm : Form
    {
        private MenuManager _menuManager;
        private UserManager _userManager;
        private Database _database;

        public ManagerForm(Database database)
        {
            InitializeComponent();
            _menuManager = new MenuManager(database);
            _userManager = new UserManager(database);
            LoadMenuItems();
            LoadUsers();
        }

        private void LoadMenuItems()
        {
            try
            {
                var menuItems = _menuManager.GetMenuItems();
                foreach (var item in menuItems)
                {
                    listViewMenu.Items.Add(new ListViewItem(new[] { item.Name, item.Category, item.Price.ToString("C"), item.IsAvailable ? "Yes" : "No" }));
                }
                MessageBox.Show("Menu items loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading menu items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsers()
        {
            try
            {
                var users = _userManager.GetUsers();
                foreach (var user in users)
                {
                    listViewUsers.Items.Add(new ListViewItem(new[] { user.Username, user.Role }));
                }
                MessageBox.Show("Users loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWelcome_Click(object sender, EventArgs e)
        {
            try
            {
                Welcome welcomeForm = new Welcome();
                welcomeForm.Show();
                this.Hide();
                MessageBox.Show("Welcome form displayed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying welcome form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void user_Click(object sender, EventArgs e)
        {
            try
            {
                var newUser = new User
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Role = cmbRole.SelectedItem.ToString()
                };
                _userManager.AddUser(newUser);
                listViewUsers.Items.Add(new ListViewItem(new[] { newUser.Username, newUser.Role }));
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Product_Click(object sender, EventArgs e)
        {
            try
            {
                var newItem = new MenuItem
                {
                    Name = txtName.Text,
                    Category = txtCategory.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    Ingredients = txtIngredients.Text,
                    IsSpicy = chkIsSpicy.Checked,
                    IsVegetarian = chkIsVegetarian.Checked,
                    IsAvailable = true
                };
                _menuManager.AddMenuItem(newItem);
                listViewMenu.Items.Add(new ListViewItem(new[] { newItem.Name, newItem.Category, newItem.Price.ToString("C"), "Yes" }));
                MessageBox.Show("Menu item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding menu item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateAvailability_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewMenu.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a menu item to update availability.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedItem = listViewMenu.SelectedItems[0];
                var itemName = selectedItem.SubItems[0].Text;

                // Toggle availability
                var isAvailable = selectedItem.SubItems[3].Text == "Yes" ? false : true;

                _menuManager.UpdateMenuItemAvailability(itemName, isAvailable);
                selectedItem.SubItems[3].Text = isAvailable ? "Yes" : "No";

                MessageBox.Show("Menu item availability updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating menu item availability: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

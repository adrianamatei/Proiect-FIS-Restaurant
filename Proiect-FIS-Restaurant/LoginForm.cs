using System;
using System.Windows.Forms;

namespace Proiect_FIS_Restaurant
{
    public partial class LoginForm : Form
    {
        private Database _database;
        private UserManager _userManager;

        public LoginForm()
        {
            InitializeComponent();
            _database = new Database();
            _userManager = new UserManager(_database);
        }

        
        private void btnWelcome_Click(object sender, EventArgs e)
        {
            Welcome welcomeForm = new Welcome(); // Create an instance of the WelcomeForm
            welcomeForm.Show(); // Show the WelcomeForm
            this.Hide(); // Hide the current form
        }

       

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtUsername.Text;
                var password = txtPassword.Text;

                // Log input for debugging purposes
                Console.WriteLine($"Attempting login with Username: {username} and Password: {password}");

                var user = _userManager.Authenticate(username, password);
                if (user != null)
                {
                    Console.WriteLine($"Login successful. User Role: {user.Role}");

                    if (user.Role == "Manager")
                    {
                        var managerForm = new ManagerForm(_database);
                        managerForm.Show();
                    }
                    else if (user.Role == "Staff")
                    {
                        var staffForm = new StaffForm(_database);
                        staffForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                // Log exception for debugging purposes
                Console.WriteLine($"Exception during login: {ex.Message}");
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}

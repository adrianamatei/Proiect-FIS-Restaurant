using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_FIS_Restaurant
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           LoginForm loginForm = new LoginForm(); // Creează o instanță a formularului WelcomeForm
           loginForm.Show(); // Afișează formularul WelcomeForm
            this.Hide(); // Ascunde formularul curent
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
           
                var database = new Database();
                var menuForm = new MenuForm(database);
                menuForm.Show();
                this.Hide(); // Ascunde form-ul curent, dacă dorești
            

        }
    }
}

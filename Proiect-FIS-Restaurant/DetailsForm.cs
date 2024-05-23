using System;
using System.Windows.Forms;

namespace Proiect_FIS_Restaurant
{
    public partial class DetailsForm : Form
    {
        private MenuItem _menuItem;

        public DetailsForm(MenuItem menuItem)
        {
            InitializeComponent();
            _menuItem = menuItem;
            LoadDetails();
        }

        private void LoadDetails()
        {
            lblName.Text = _menuItem.Name;
            lblCategory.Text = _menuItem.Category;
            lblIsPrice.Text = _menuItem.Price.ToString("C");
            lblIngredients.Text = _menuItem.Ingredients;
            lblIsSpicy.Text = _menuItem.IsSpicy ? "Yes" : "No";
            lblIsVegetarian.Text = _menuItem.IsVegetarian ? "Yes" : "No";
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

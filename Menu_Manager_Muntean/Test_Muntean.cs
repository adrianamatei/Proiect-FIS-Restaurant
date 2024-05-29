using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proiect_FIS_Restaurant;
using Proiect_FIS_Restaurant.Proiect_FIS_Restaurant;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Alias pentru a diferenția între MenuItem din System.Windows.Forms și cel din Proiect_FIS_Restaurant
using MenuItem = Proiect_FIS_Restaurant.MenuItem;

namespace Menu_Manager_Muntean
{
    [TestClass]
    public class Test_Muntean
    {
        private Mock<Database>? _mockDatabase;
        private Mock<MenuManager>? _mockMenuManager;
        private ManagerForm? _managerForm;

        [TestInitialize]
        public void Setup()
        {
            _mockDatabase = new Mock<Database>();
            _mockMenuManager = new Mock<MenuManager>(_mockDatabase.Object);
            _managerForm = new ManagerForm(_mockDatabase.Object);
            _managerForm.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerForm?.Close();
        }

        [TestMethod]
        public void Product_Click_ShouldAddNewMenuItem()
        {
            // Arrange
            var newItem = new MenuItem
            {
                Name = "Pasta",
                Category = "Main Course",
                Price = 12.99m,
                Ingredients = "Tomato, Pasta, Cheese",
                IsSpicy = false,
                IsVegetarian = true,
                IsAvailable = true
            };
            _mockMenuManager?.Setup(m => m.AddMenuItem(newItem));

            var txtName = new TextBox { Name = "txtName", Text = newItem.Name };
            var txtCategory = new TextBox { Name = "txtCategory", Text = newItem.Category };
            var txtPrice = new TextBox { Name = "txtPrice", Text = newItem.Price.ToString() };
            var txtIngredients = new TextBox { Name = "txtIngredients", Text = newItem.Ingredients };
            var chkIsSpicy = new CheckBox { Name = "chkIsSpicy", Checked = newItem.IsSpicy };
            var chkIsVegetarian = new CheckBox { Name = "chkIsVegetarian", Checked = newItem.IsVegetarian };

            _managerForm?.Controls.Add(txtName);
            _managerForm?.Controls.Add(txtCategory);
            _managerForm?.Controls.Add(txtPrice);
            _managerForm?.Controls.Add(txtIngredients);
            _managerForm?.Controls.Add(chkIsSpicy);
            _managerForm?.Controls.Add(chkIsVegetarian);

            if (_managerForm != null && _mockMenuManager != null)
            {
                PrivateObject privateObject = new PrivateObject(_managerForm);
                privateObject.SetField("_menuManager", _mockMenuManager.Object);

                // Act
                privateObject.Invoke("Product_Click", new object[] { this, EventArgs.Empty });

                // Assert
                _mockMenuManager.Verify(m => m.AddMenuItem(It.IsAny<MenuItem>()), Times.Once);
            }
        }

        [TestMethod]
        public void LoadMenuItems_ShouldLoadMenuItemsCorrectly()
        {
            // Arrange
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Name = "Pasta", Category = "Main Course", Price = 12.99m, IsAvailable = true },
                new MenuItem { Name = "Soup", Category = "Starter", Price = 5.99m, IsAvailable = false }
            };
            _mockMenuManager?.Setup(m => m.GetMenuItems()).Returns(menuItems);

            if (_managerForm != null && _mockMenuManager != null)
            {
                PrivateObject privateObject = new PrivateObject(_managerForm);
                privateObject.SetField("_menuManager", _mockMenuManager.Object);

                // Act
                privateObject.Invoke("LoadMenuItems");

                // Assert
                var listView = (ListView?)privateObject.GetField("listViewMenu");
                Assert.IsNotNull(listView);
                if (listView != null)
                {
                    Assert.AreEqual(2, listView.Items.Count);
                    Assert.AreEqual("Pasta", listView.Items[0].SubItems[0].Text);
                    Assert.AreEqual("Main Course", listView.Items[0].SubItems[1].Text);
                    Assert.AreEqual("12.99", listView.Items[0].SubItems[2].Text);
                    Assert.AreEqual("Yes", listView.Items[0].SubItems[3].Text);
                }
            }
        }

        [TestMethod]
        public void BtnUpdateAvailability_Click_ShouldUpdateAvailability()
        {
            // Arrange
            var menuItem = new MenuItem { Name = "Pasta", IsAvailable = true };
            _mockMenuManager?.Setup(m => m.UpdateMenuItemAvailability(menuItem.Name, false));

            var listView = new ListView();
            var listViewItem = new ListViewItem(new[] { menuItem.Name, "Main Course", "12.99", "Yes" });
            listView.Items.Add(listViewItem);
            listView.Items[0].Selected = true;
            _managerForm?.Controls.Add(listView);

            if (_managerForm != null && _mockMenuManager != null)
            {
                PrivateObject privateObject = new PrivateObject(_managerForm);
                privateObject.SetField("listViewMenu", listView);
                privateObject.SetField("_menuManager", _mockMenuManager.Object);

                // Act
                privateObject.Invoke("btnUpdateAvailability_Click", new object[] { this, EventArgs.Empty });

                // Assert
                _mockMenuManager.Verify(m => m.UpdateMenuItemAvailability(menuItem.Name, false), Times.Once);
                Assert.AreEqual("No", listView.Items[0].SubItems[3].Text);
            }
        }

        [TestMethod]
        public void Product_Click_ShouldShowErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            _mockMenuManager?.Setup(m => m.AddMenuItem(It.IsAny<MenuItem>())).Throws(new Exception("Test Exception"));

            var txtName = new TextBox { Name = "txtName", Text = "Pasta" };
            var txtCategory = new TextBox { Name = "txtCategory", Text = "Main Course" };
            var txtPrice = new TextBox { Name = "txtPrice", Text = "12.99" };
            var txtIngredients = new TextBox { Name = "txtIngredients", Text = "Tomato, Pasta, Cheese" };
            var chkIsSpicy = new CheckBox { Name = "chkIsSpicy", Checked = false };
            var chkIsVegetarian = new CheckBox { Name = "chkIsVegetarian", Checked = true };

            _managerForm?.Controls.Add(txtName);
            _managerForm?.Controls.Add(txtCategory);
            _managerForm?.Controls.Add(txtPrice);
            _managerForm?.Controls.Add(txtIngredients);
            _managerForm?.Controls.Add(chkIsSpicy);
            _managerForm?.Controls.Add(chkIsVegetarian);

            if (_managerForm != null && _mockMenuManager != null)
            {
                PrivateObject privateObject = new PrivateObject(_managerForm);
                privateObject.SetField("_menuManager", _mockMenuManager.Object);

                // Act
                privateObject.Invoke("Product_Click", new object[] { this, EventArgs.Empty });

                // Assert
                var messageBoxText = privateObject.Invoke("GetLastError") as string ?? string.Empty;
                Assert.IsTrue(messageBoxText.Contains("Test Exception"));
            }
        }

        [TestMethod]
        public void BtnWelcome_Click_ShouldNavigateToWelcomeForm()
        {
            // Arrange
            if (_managerForm != null)
            {
                PrivateObject privateObject = new PrivateObject(_managerForm);

                // Act
                privateObject.Invoke("btnWelcome_Click", new object[] { this, EventArgs.Empty });

                // Assert
                var welcomeForm = Application.OpenForms["Welcome"];
                Assert.IsNotNull(welcomeForm);
                Assert.IsFalse(_managerForm.Visible);
            }
        }
    }
}

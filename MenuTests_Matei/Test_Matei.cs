using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proiect_FIS_Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WinForms = System.Windows.Forms;

namespace MenuTests_Matei
{
    [TestClass]
    public class Test_Matei
    {
        private Mock<Database>? _databaseMock;
        private MenuManager? _menuManager;
        private OrderManager? _orderManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _databaseMock = new Mock<Database>("TestConnectionString");

            _databaseMock.Setup(db => db.GetMenuItems()).Returns(new List<MenuItem>
    {
        new MenuItem { Name = "Item1", Category = "Category1", Price = 10, Ingredients = "Ingredients1", IsSpicy = false, IsVegetarian = true, IsAvailable = true },
        new MenuItem { Name = "Item2", Category = "Category2", Price = 15, Ingredients = "Ingredients2", IsSpicy = true, IsVegetarian = false, IsAvailable = true }
    });

            _menuManager = new MenuManager(_databaseMock.Object);
            _orderManager = new OrderManager(_databaseMock.Object);
        }


        [TestMethod]
        public void TestLoadMenuItems()
        {
            var form = new MenuForm(_databaseMock!.Object);
            Assert.IsNotNull(form, "MenuForm instance is null.");

            MethodInfo loadMenuItemsMethod = typeof(MenuForm).GetMethod("LoadMenuItems", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(loadMenuItemsMethod, "LoadMenuItems method not found.");

            loadMenuItemsMethod!.Invoke(form, null);

            var listViewMenu = (WinForms.ListView?)typeof(MenuForm).GetField("listViewMenu", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(form);
            Assert.IsNotNull(listViewMenu, "listViewMenu field not found.");

            Assert.AreEqual(_menuManager?.GetMenuItems().Count, listViewMenu?.Items.Count);
        }

        [TestMethod]
        public void BtnWelcome_Click_ShouldNavigateToWelcomeForm()
        {
            var form = new MenuForm(_databaseMock!.Object);
            Assert.IsNotNull(form, "MenuForm instance is null.");

            MethodInfo btnWelcome_ClickMethod = typeof(MenuForm).GetMethod("btnWelcome_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(btnWelcome_ClickMethod, "btnWelcome_Click method not found.");

            btnWelcome_ClickMethod!.Invoke(form, new object[] { this, EventArgs.Empty });

            var welcomeForm = WinForms.Application.OpenForms.OfType<Welcome>().FirstOrDefault();
            Assert.IsNotNull(welcomeForm, "Welcome form instance is null.");
            Assert.IsFalse(form.Visible);
        }

        [TestMethod]
        public void Cart_Click_ShouldAddItemToCart()
        {
            var form = new MenuForm(_databaseMock!.Object);
            Assert.IsNotNull(form, "MenuForm instance is null.");

            var listViewMenu = (WinForms.ListView?)typeof(MenuForm).GetField("listViewMenu", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(form);
            Assert.IsNotNull(listViewMenu, "listViewMenu field not found.");

            listViewMenu!.Items.Add(new WinForms.ListViewItem(new[] { "Item1", "Category1", "10", "Ingredients1", "No", "Yes", "Yes" }));
            listViewMenu.Items[0].Selected = true;

            MethodInfo cart_ClickMethod = typeof(MenuForm).GetMethod("Cart_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(cart_ClickMethod, "Cart_Click method not found.");

            cart_ClickMethod!.Invoke(form, new object[] { this, EventArgs.Empty });

            var cart = (List<MenuItem>?)typeof(MenuForm).GetField("_cart", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(form);
            Assert.IsNotNull(cart, "_cart field not found.");

            Assert.AreEqual(1, cart?.Count);
            Assert.AreEqual("Item1", cart?[0].Name);
        }

        [TestMethod]
        public void Details_Click_ShouldShowDetailsForm()
        {
            var form = new MenuForm(_databaseMock!.Object);
            Assert.IsNotNull(form, "MenuForm instance is null.");

            var listViewMenu = (WinForms.ListView?)typeof(MenuForm).GetField("listViewMenu", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(form);
            Assert.IsNotNull(listViewMenu, "listViewMenu field not found.");

            listViewMenu!.Items.Add(new WinForms.ListViewItem(new[] { "Item1", "Category1", "10", "Ingredients1", "No", "Yes", "Yes" }));
            listViewMenu.Items[0].Selected = true;

            MethodInfo details_ClickMethod = typeof(MenuForm).GetMethod("details_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(details_ClickMethod, "details_Click method not found.");

            details_ClickMethod!.Invoke(form, new object[] { this, EventArgs.Empty });

            var detailsForm = WinForms.Application.OpenForms.OfType<DetailsForm>().FirstOrDefault();
            Assert.IsNotNull(detailsForm, "Details form instance is null.");
        }

        [TestMethod]
        public void Order_Click_ShouldPlaceOrder()
        {
            var form = new MenuForm(_databaseMock!.Object);
            Assert.IsNotNull(form, "MenuForm instance is null.");

            var cartField = typeof(MenuForm).GetField("_cart", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(cartField, "_cart field not found.");

            var cart = (List<MenuItem>?)cartField?.GetValue(form);
            Assert.IsNotNull(cart, "Cart instance is null.");

            cart!.Add(new MenuItem { MenuItemId = 1, Name = "Item1" });

            MethodInfo order_ClickMethod = typeof(MenuForm).GetMethod("order_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(order_ClickMethod, "order_Click method not found.");

            order_ClickMethod!.Invoke(form, new object[] { this, EventArgs.Empty });

            var orders = _orderManager?.GetAllOrders();
            Assert.IsNotNull(orders, "Orders instance is null.");
            Assert.AreEqual(1, orders?.Count);
            Assert.AreEqual("Item1", orders?[0].Items[0].Name);
            Assert.AreEqual(0, cart?.Count);
        }
    }
}

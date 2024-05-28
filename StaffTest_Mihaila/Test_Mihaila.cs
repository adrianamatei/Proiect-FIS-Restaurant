using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proiect_FIS_Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StaffTest_Mihaila
{
    [TestClass]
    public class StaffFormTests
    {
        private Mock<Database> _mockDatabase;
        private Mock<OrderManager> _mockOrderManager;
        private Mock<MenuManager> _mockMenuManager;
        private StaffForm _staffForm;

        [TestInitialize]
        public void Setup()
        {
            _mockDatabase = new Mock<Database>();
            _mockOrderManager = new Mock<OrderManager>(_mockDatabase.Object);
            _mockMenuManager = new Mock<MenuManager>(_mockDatabase.Object);
            _staffForm = new StaffForm(_mockDatabase.Object);
            _staffForm.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _staffForm.Close();
        }

        [TestMethod]
        public void LoadAllOrders_ShouldLoadOrdersCorrectly()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { OrderId = 1, Status = "Pending", EstimatedTime = DateTime.Now.AddMinutes(30), Items = new List<OrderItem> { new OrderItem { MenuItemId = 1, Name = "Pizza", Status = "Ready" } } },
                new Order { OrderId = 2, Status = "Completed", EstimatedTime = DateTime.Now.AddMinutes(45), Items = new List<OrderItem> { new OrderItem { MenuItemId = 2, Name = "Burger", Status = "Preparing" } } }
            };
            _mockOrderManager.Setup(m => m.GetAllOrders()).Returns(orders);
            PrivateObject privateObject = new PrivateObject(_staffForm);
            privateObject.SetField("_orderManager", _mockOrderManager.Object);

            // Act
            privateObject.Invoke("LoadAllOrders");

            // Assert
            var listView = (ListView)privateObject.GetField("listViewOrders");
            Assert.AreEqual(2, listView.Items.Count);
            Assert.AreEqual("1", listView.Items[0].SubItems[0].Text);
            Assert.AreEqual("Pizza (Ready)", listView.Items[0].SubItems[3].Text);
        }

        [TestMethod]
        public void Payment_Click_ShouldConfirmPayment()
        {
            // Arrange
            var order = new Order { OrderId = 1, Status = "Pending", EstimatedTime = DateTime.Now.AddMinutes(30), Items = new List<OrderItem> { new OrderItem { MenuItemId = 1, Name = "Pizza", Status = "Ready" } } };
            _mockOrderManager.Setup(m => m.GetAllOrders()).Returns(new List<Order> { order });
            PrivateObject privateObject = new PrivateObject(_staffForm);
            privateObject.SetField("_orderManager", _mockOrderManager.Object);

            privateObject.Invoke("LoadAllOrders");

            var listView = (ListView)privateObject.GetField("listViewOrders");
            listView.Items[0].Selected = true;
            var txtReceiptNumber = new TextBox { Name = "txtReceiptNumber", Text = "12345" };
            privateObject.SetField("txtReceiptNumber", txtReceiptNumber);

            // Act
            privateObject.Invoke("payment_Click", this, EventArgs.Empty);

            // Assert
            _mockOrderManager.Verify(m => m.ConfirmOrderPayment(1, 12345), Times.Once);
            Assert.AreEqual(0, listView.Items.Count);
        }

        [TestMethod]
        public void Update_Click_ShouldUpdateOrderStatus()
        {
            // Arrange
            var order = new Order { OrderId = 1, Status = "Pending", EstimatedTime = DateTime.Now.AddMinutes(30), Items = new List<OrderItem> { new OrderItem { MenuItemId = 1, Name = "Pizza", Status = "Ready" } } };
            _mockOrderManager.Setup(m => m.GetAllOrders()).Returns(new List<Order> { order });
            PrivateObject privateObject = new PrivateObject(_staffForm);
            privateObject.SetField("_orderManager", _mockOrderManager.Object);

            privateObject.Invoke("LoadAllOrders");

            var listView = (ListView)privateObject.GetField("listViewOrders");
            listView.Items[0].Selected = true;
            var cmbOrderStatus = new ComboBox { Name = "cmbOrderStatus" };
            cmbOrderStatus.Items.Add("Completed");
            cmbOrderStatus.SelectedIndex = 0;
            privateObject.SetField("cmbOrderStatus", cmbOrderStatus);

            // Act
            privateObject.Invoke("update_Click", this, EventArgs.Empty);

            // Assert
            _mockOrderManager.Verify(m => m.UpdateOrderStatus(1, "Completed"), Times.Once);
            Assert.AreEqual("Completed", listView.Items[0].SubItems[1].Text);
        }

        [TestMethod]
        public void BtnBack_Click_ShouldNavigateToLoginForm()
        {
            // Arrange
            PrivateObject privateObject = new PrivateObject(_staffForm);
            var loginFormMock = new Mock<LoginForm>();
            privateObject.SetField("_loginForm", loginFormMock.Object);

            // Act
            privateObject.Invoke("btnBack_Click", this, EventArgs.Empty);

            // Assert
            Assert.IsFalse(_staffForm.Visible);
            loginFormMock.Verify(m => m.Show(), Times.Once);
        }

        [TestMethod]
        public void Payment_Click_ShouldShowErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            var order = new Order { OrderId = 1, Status = "Pending", EstimatedTime = DateTime.Now.AddMinutes(30), Items = new List<OrderItem> { new OrderItem { MenuItemId = 1, Name = "Pizza", Status = "Ready" } } };
            _mockOrderManager.Setup(m => m.GetAllOrders()).Returns(new List<Order> { order });
            _mockOrderManager.Setup(m => m.ConfirmOrderPayment(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Test Exception"));
            PrivateObject privateObject = new PrivateObject(_staffForm);
            privateObject.SetField("_orderManager", _mockOrderManager.Object);

            privateObject.Invoke("LoadAllOrders");

            var listView = (ListView)privateObject.GetField("listViewOrders");
            listView.Items[0].Selected = true;
            var txtReceiptNumber = new TextBox { Name = "txtReceiptNumber", Text = "12345" };
            privateObject.SetField("txtReceiptNumber", txtReceiptNumber);

            // Act
            privateObject.Invoke("payment_Click", this, EventArgs.Empty);

            // Assert
            var messageBoxText = privateObject.Invoke("GetLastError") as string;
            Assert.IsTrue(messageBoxText.Contains("Test Exception"));
        }
    }
}

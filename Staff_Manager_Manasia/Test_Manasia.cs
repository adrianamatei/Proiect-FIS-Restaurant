using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proiect_FIS_Restaurant;
using Proiect_FIS_Restaurant.Proiect_FIS_Restaurant;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Staff_Manager_Manasia
{
    [TestClass]
    public class Test_Manasia
    {
        private Mock<Database> _mockDatabase;
        private Mock<UserManager> _mockUserManager;
        private ManagerForm _managerForm;

        [TestInitialize]
        public void Setup()
        {
            _mockDatabase = new Mock<Database>();
            _mockUserManager = new Mock<UserManager>(_mockDatabase.Object);
            _managerForm = new ManagerForm(_mockDatabase.Object);
            _managerForm.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerForm.Close();
        }

        [TestMethod]
        public void AddUser_Click_ShouldAddNewUser()
        {
            // Arrange
            var newUser = new User
            {
                Username = "newuser",
                Password = "password",
                Role = "Staff"
            };
            _mockUserManager.Setup(m => m.AddUser(newUser));

            var txtUsername = new TextBox { Name = "txtUsername", Text = newUser.Username };
            var txtPassword = new TextBox { Name = "txtPassword", Text = newUser.Password };
            var cmbRole = new ComboBox { Name = "cmbRole" };
            cmbRole.Items.Add("Staff");
            cmbRole.SelectedIndex = 0;

            _managerForm.Controls.Add(txtUsername);
            _managerForm.Controls.Add(txtPassword);
            _managerForm.Controls.Add(cmbRole);

            PrivateObject privateObject = new PrivateObject(_managerForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("user_Click", this, EventArgs.Empty);

            // Assert
            _mockUserManager.Verify(m => m.AddUser(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void LoadUsers_ShouldLoadUsersCorrectly()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Username = "manager", Role = "Manager" },
                new User { Username = "staff", Role = "Staff" }
            };
            _mockUserManager.Setup(m => m.GetUsers()).Returns(users);
            PrivateObject privateObject = new PrivateObject(_managerForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("LoadUsers");

            // Assert
            var listView = (ListView)privateObject.GetField("listViewUsers");
            Assert.AreEqual(2, listView.Items.Count);
            Assert.AreEqual("manager", listView.Items[0].SubItems[0].Text);
            Assert.AreEqual("Manager", listView.Items[0].SubItems[1].Text);
            Assert.AreEqual("staff", listView.Items[1].SubItems[0].Text);
            Assert.AreEqual("Staff", listView.Items[1].SubItems[1].Text);
        }

        [TestMethod]
        public void AddUser_Click_ShouldShowErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            _mockUserManager.Setup(m => m.AddUser(It.IsAny<User>())).Throws(new Exception("Test Exception"));

            var txtUsername = new TextBox { Name = "txtUsername", Text = "newuser" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "password" };
            var cmbRole = new ComboBox { Name = "cmbRole" };
            cmbRole.Items.Add("Staff");
            cmbRole.SelectedIndex = 0;

            _managerForm.Controls.Add(txtUsername);
            _managerForm.Controls.Add(txtPassword);
            _managerForm.Controls.Add(cmbRole);

            PrivateObject privateObject = new PrivateObject(_managerForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("user_Click", this, EventArgs.Empty);

            // Assert
            var messageBoxText = privateObject.Invoke("GetLastError") as string;
            Assert.IsTrue(messageBoxText.Contains("Test Exception"));
        }

        [TestMethod]
        public void BtnWelcome_Click_ShouldNavigateToWelcomeForm()
        {
            // Arrange
            PrivateObject privateObject = new PrivateObject(_managerForm);

            // Act
            privateObject.Invoke("btnWelcome_Click", this, EventArgs.Empty);

            // Assert
            var welcomeForm = Application.OpenForms["Welcome"];
            Assert.IsNotNull(welcomeForm);
            Assert.IsFalse(_managerForm.Visible);
        }

        [TestMethod]
        public void LoadUsers_ShouldShowErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            _mockUserManager.Setup(m => m.GetUsers()).Throws(new Exception("Test Exception"));
            PrivateObject privateObject = new PrivateObject(_managerForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("LoadUsers");

            // Assert
            var messageBoxText = privateObject.Invoke("GetLastError") as string;
            Assert.IsTrue(messageBoxText.Contains("Test Exception"));
        }
    }
}

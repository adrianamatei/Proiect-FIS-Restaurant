using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proiect_FIS_Restaurant;
using Proiect_FIS_Restaurant.Proiect_FIS_Restaurant;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoginTest_Gug
{
    [TestClass]
    public class Test_Gug
    {
        private Mock<Database> _mockDatabase;
        private Mock<UserManager> _mockUserManager;
        private LoginForm _loginForm;

        [TestInitialize]
        public void Setup()
        {
            _mockDatabase = new Mock<Database>();
            _mockUserManager = new Mock<UserManager>(_mockDatabase.Object);
            _loginForm = new LoginForm();
            _loginForm.Show();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _loginForm.Close();
        }

        [TestMethod]
        public void Login_Click_ShouldAuthenticateManager()
        {
            // Arrange
            var user = new User { Username = "manager", Password = "password", Role = "Manager" };
            _mockUserManager.Setup(m => m.Authenticate("manager", "password")).Returns(user);

            var txtUsername = new TextBox { Name = "txtUsername", Text = "manager" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "password" };
            _loginForm.Controls.Add(txtUsername);
            _loginForm.Controls.Add(txtPassword);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("login_Click", this, EventArgs.Empty);

            // Assert
            _mockUserManager.Verify(m => m.Authenticate("manager", "password"), Times.Once);
        }

        [TestMethod]
        public void Login_Click_ShouldAuthenticateStaff()
        {
            // Arrange
            var user = new User { Username = "staff", Password = "password", Role = "Staff" };
            _mockUserManager.Setup(m => m.Authenticate("staff", "password")).Returns(user);

            var txtUsername = new TextBox { Name = "txtUsername", Text = "staff" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "password" };
            _loginForm.Controls.Add(txtUsername);
            _loginForm.Controls.Add(txtPassword);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("login_Click", this, EventArgs.Empty);

            // Assert
            _mockUserManager.Verify(m => m.Authenticate("staff", "password"), Times.Once);
        }

        [TestMethod]
        public void Login_Click_ShouldShowErrorMessage_WhenAuthenticationFails()
        {
            // Arrange
            _mockUserManager.Setup(m => m.Authenticate("user", "wrongpassword")).Returns((User)null);

            var txtUsername = new TextBox { Name = "txtUsername", Text = "user" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "wrongpassword" };
            _loginForm.Controls.Add(txtUsername);
            _loginForm.Controls.Add(txtPassword);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("login_Click", this, EventArgs.Empty);

            // Assert
            var messageBoxText = privateObject.Invoke("GetLastError") as string;
            Assert.IsTrue(messageBoxText.Contains("Invalid username or password."));
        }

        [TestMethod]
        public void Login_Click_ShouldHandleException()
        {
            // Arrange
            _mockUserManager.Setup(m => m.Authenticate("user", "password")).Throws(new Exception("Test Exception"));

            var txtUsername = new TextBox { Name = "txtUsername", Text = "user" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "password" };
            _loginForm.Controls.Add(txtUsername);
            _loginForm.Controls.Add(txtPassword);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("login_Click", this, EventArgs.Empty);

            // Assert
            var messageBoxText = privateObject.Invoke("GetLastError") as string;
            Assert.IsTrue(messageBoxText.Contains("An error occurred: Test Exception"));
        }

        [TestMethod]
        public void BtnWelcome_Click_ShouldNavigateToWelcomeForm()
        {
            // Arrange
            PrivateObject privateObject = new PrivateObject(_loginForm);

            // Act
            privateObject.Invoke("btnWelcome_Click", this, EventArgs.Empty);

            // Assert
            var welcomeForm = Application.OpenForms["Welcome"];
            Assert.IsNotNull(welcomeForm);
            Assert.IsFalse(_loginForm.Visible);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proiect_FIS_Restaurant;
using Proiect_FIS_Restaurant.Proiect_FIS_Restaurant;
using System;
using System.Reflection;
using System.Security.Authentication;
using System.Windows.Forms;

namespace LoginTest_Gug
{
    [TestClass]
    public class Test_Gug
    {
        private Mock<Database>? _mockDatabase;
        private Mock<UserManager>? _mockUserManager;
        private LoginForm? _loginForm;

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
            _loginForm?.Close();
        }

        [TestMethod]
        public void Login_Click_ShouldAuthenticateManager()
        {
            // Arrange
            var user = new User { Username = "manager", Role = "Manager" };
            _mockUserManager?.Setup(m => m.Authenticate("manager", "password")).Returns(user);

            var txtUsername = new TextBox { Name = "txtUsername", Text = "manager" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "password" };
            _loginForm?.Controls.Add(txtUsername);
            _loginForm?.Controls.Add(txtPassword);

            Assert.IsNotNull(_loginForm);
            Assert.IsNotNull(_mockUserManager);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("login_Click", new object[] { this, EventArgs.Empty });

            // Assert
            _mockUserManager.Verify(m => m.Authenticate("manager", "password"), Times.Once);
        }

        [TestMethod]
        public void Login_Click_ShouldAuthenticateStaff()
        {
            // Arrange
            var user = new User { Username = "staff", Role = "Staff" };
            _mockUserManager?.Setup(m => m.Authenticate("staff", "password")).Returns(user);

            var txtUsername = new TextBox { Name = "txtUsername", Text = "staff" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "password" };
            _loginForm?.Controls.Add(txtUsername);
            _loginForm?.Controls.Add(txtPassword);

            Assert.IsNotNull(_loginForm);
            Assert.IsNotNull(_mockUserManager);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act
            privateObject.Invoke("login_Click", new object[] { this, EventArgs.Empty });

            // Assert
            _mockUserManager.Verify(m => m.Authenticate("staff", "password"), Times.Once);
        }

        [TestMethod]
        public void Login_Click_ShouldShowErrorMessage_WhenAuthenticationFails()
        {
            // Arrange
            _mockUserManager?.Setup(m => m.Authenticate("invalidUser", "invalidPassword")).Throws<InvalidCredentialException>();

            var txtUsername = new TextBox { Name = "txtUsername", Text = "invalidUser" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "invalidPassword" };
            _loginForm?.Controls.Add(txtUsername);
            _loginForm?.Controls.Add(txtPassword);

            Assert.IsNotNull(_loginForm);
            Assert.IsNotNull(_mockUserManager);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act & Assert
            Assert.ThrowsException<InvalidCredentialException>(() => privateObject.Invoke("login_Click", new object[] { this, EventArgs.Empty }));
        }

        [TestMethod]
        public void Login_Click_ShouldHandleException()
        {
            // Arrange
            _mockUserManager?.Setup(m => m.Authenticate("user", "password")).Throws(new Exception("Test Exception"));

            var txtUsername = new TextBox { Name = "txtUsername", Text = "user" };
            var txtPassword = new TextBox { Name = "txtPassword", Text = "password" };
            _loginForm?.Controls.Add(txtUsername);
            _loginForm?.Controls.Add(txtPassword);

            Assert.IsNotNull(_loginForm);
            Assert.IsNotNull(_mockUserManager);

            PrivateObject privateObject = new PrivateObject(_loginForm);
            privateObject.SetField("_userManager", _mockUserManager.Object);

            // Act & Assert
            var ex = Assert.ThrowsException<TargetInvocationException>(() => privateObject.Invoke("login_Click", new object[] { this, EventArgs.Empty }));
            Assert.IsInstanceOfType(ex.InnerException, typeof(Exception));
            Assert.AreEqual("Test Exception", ex.InnerException.Message);
        }

        [TestMethod]
        public void BtnWelcome_Click_ShouldNavigateToWelcomeForm()
        {
            // Arrange
            Assert.IsNotNull(_loginForm);

            PrivateObject privateObject = new PrivateObject(_loginForm);

            // Act
            privateObject.Invoke("btnWelcome_Click", new object[] { this, EventArgs.Empty });

            // Assert
            var welcomeForm = Application.OpenForms["Welcome"];
            Assert.IsNotNull(welcomeForm);
            Assert.IsFalse(_loginForm.Visible);
        }
    }
}

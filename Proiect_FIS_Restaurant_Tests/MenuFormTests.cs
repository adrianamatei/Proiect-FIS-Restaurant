
using Proiect_FIS_Restaurant;
using System.Collections.Generic;
using System.Linq;

namespace Proiect_FIS_Restaurant_Tests
{
    [TestClass]
    public class MenuFormTests
    {
        [TestMethod]
        public void LoadMenuItems_ShouldLoadItemsIntoListView()
        {
            // Arrange
            var mockDatabase = new Mock<Database>();
            mockDatabase.Setup(db => db.GetMenuItems()).Returns(new List<MenuItem>
            {
                new MenuItem { Name = "Pizza", Category = "Food", Price = 10.99M, Ingredients = "Cheese, Tomato", IsSpicy = false, IsVegetarian = false, IsAvailable = true },
                new MenuItem { Name = "Pasta", Category = "Food", Price = 8.99M, Ingredients = "Pasta, Sauce", IsSpicy = false, IsVegetarian = true, IsAvailable = true }
            });
            var form = new MenuForm(mockDatabase.Object);

            // Act
            form.LoadMenuItems();

            // Assert
            Assert.AreEqual(2, form.listViewMenu.Items.Count);
            Assert.AreEqual("Pizza", form.listViewMenu.Items[0].SubItems[0].Text);
            Assert.AreEqual("Pasta", form.listViewMenu.Items[1].SubItems[0].Text);
        }
    }
}

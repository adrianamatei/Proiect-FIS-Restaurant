using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace Proiect_FIS_Restaurant.Tests
{
    public class MenuFormTests
    {
        [Fact]
        public void LoadMenuItems_ShouldLoadItemsFromDatabase()
        {
            // Arrange
            var mockDatabase = new Mock<Database>();
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Name = "Pizza", Category = "Main", Price = 9.99M, Ingredients = "Cheese, Tomato", IsSpicy = false, IsVegetarian = true, IsAvailable = true },
                new MenuItem { Name = "Burger", Category = "Main", Price = 12.99M, Ingredients = "Beef, Lettuce", IsSpicy = false, IsVegetarian = false, IsAvailable = true }
            };
            mockDatabase.Setup(db => db.GetMenuItems()).Returns(menuItems);

            var form = new MenuForm(mockDatabase.Object);

            // Utilizează reflecția pentru a apela metoda LoadMenuItems
            MethodInfo loadMenuItemsMethod = typeof(MenuForm).GetMethod("LoadMenuItems", BindingFlags.NonPublic | BindingFlags.Instance);
            loadMenuItemsMethod.Invoke(form, null);

            // Utilizează reflecția pentru a accesa listViewMenu
            FieldInfo listViewMenuField = typeof(MenuForm).GetField("listViewMenu", BindingFlags.NonPublic | BindingFlags.Instance);
            var listViewMenu = (ListView)listViewMenuField.GetValue(form);

            // Assert
            Assert.Equal(2, listViewMenu.Items.Count);
            Assert.Equal("Pizza", listViewMenu.Items[0].SubItems[0].Text);
            Assert.Equal("Burger", listViewMenu.Items[1].SubItems[0].Text);
        }

        // Restul testelor ar trebui să urmeze un model similar, utilizând reflecția pentru a accesa membrii privați
    }
}

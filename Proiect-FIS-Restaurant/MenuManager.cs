using Proiect_FIS_Restaurant;
using System.Collections.Generic;

public class MenuManager
{
    private Database _database;

    public MenuManager(Database database)
    {
        _database = database;
    }

    public List<MenuItem> GetMenuItems()
    {
        return _database.GetMenuItems();
    }

    public void AddMenuItem(MenuItem menuItem)
    {
        _database.AddMenuItem(menuItem);
    }

    public void UpdateMenuItemAvailability(string itemName, bool isAvailable)
    {
        _database.UpdateMenuItemAvailability(itemName, isAvailable);
    }
}

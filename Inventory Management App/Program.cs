
using Inventory_Management_App;

internal class Program
{
    private static void Main()
    {
        InventoryManagement inventoryManagement = new();
        inventoryManagement.CreateDatabaseAsync().Wait();
    }
}
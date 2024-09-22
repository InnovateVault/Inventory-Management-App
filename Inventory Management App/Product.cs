namespace Inventory_Management_App;
internal class Product
{
    private string Name { get; set; }
    private int Quantity { get; set; }
    private double Price { get; set; }
    public Product(string name, int quantity, double price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }
}

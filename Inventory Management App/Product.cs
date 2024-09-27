namespace Inventory_Management_App;
internal class Product
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int ID { get; set; }
    public double Price { get; set; }

    public Product(string name, int quantity, int id, double price)
    {
        Name = name;
        Quantity = quantity;
        ID = id;
        Price = price;
    }

    public Product() { }
}

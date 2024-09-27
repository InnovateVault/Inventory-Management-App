using Dapper;
using System.Data.SQLite;


namespace Inventory_Management_App;
internal class InventoryManagement
{
    private readonly string connectionString = "Data Source=InventoryDatabase.db;Version=3;";


    public async Task CreateDatabase()
    {
        try
        {
            using var connection = new SQLiteConnection(connectionString);
            await connection.OpenAsync();

            var createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Inventory (
                Name TEXT NOT NULL,
                Quantity INTEGER NOT NULL,
                ID INTEGER NOT NULL PRIMARY KEY,
                Price REAL NOT NULL
            );";

            await connection.ExecuteAsync(createTableQuery);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void AddItem(Product product)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();
        var sql = "INSERT INTO Inventory (Name, Quantity, ID, Price) VALUES (@Name, @Quantity, @ID, @Price)";
        connection.Execute(sql, new { product.Name, product.Quantity, product.ID, product.Price });
    }

    public void RestockItem(int id, int amount)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();
        var sql = "UPDATE Inventory SET Quantity = Quantity + @Amount WHERE ID = @ID";
        var itemChanged = connection.Execute(sql, new { Amount = amount, ID = id });

        if (itemChanged == 0)
        {
            throw new InvalidOperationException("Item Not Found");
        }
    }

    public void SaleItem(int id, int amount)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();
        var sql = "UPDATE Inventory SET Quantity = Quantity - @Amount WHERE ID = @ID";
        var itemChanged = connection.Execute(sql, new { Amount = amount, ID = id });

        if (itemChanged == 0)
        {
            throw new InvalidOperationException("Item Not Found");
        }
    }

    public void UpdateItem(int id, string name, decimal? priceUpdate)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        var sql = "UPDATE Inventory SET ";
        var parameters = new DynamicParameters();
        parameters.Add("ID", id);

        if (!string.IsNullOrEmpty(name))
        {
            sql += "Name = @Name, ";
            parameters.Add("Name", name);
        }

        if (priceUpdate.HasValue)
        {
            sql += "Price = @Price, ";
            parameters.Add("Price", priceUpdate.Value);
        }

        sql = sql.TrimEnd(',', ' ');

        sql += " WHERE ID = @ID";

        var itemChanged = connection.Execute(sql, parameters);

        if (itemChanged == 0)
        {
            throw new InvalidOperationException("Item Not Found");
        }
    }

    public Product ViewInventory(int id)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();
        var sql = "SELECT Name, Quantity, Price, ID FROM Inventory WHERE ID = @ID";
        var product = connection.QuerySingleOrDefault<Product>(sql, new { ID = id });

        return product ?? throw new InvalidOperationException("Product not found.");
    }

    public void DeleteItem(int id)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();
        var sql = "DELETE FROM Inventory WHERE ID = @ID";
        var itemChanged = connection.Execute(sql, new { ID = id});

        if (itemChanged == 0)
        {
            throw new InvalidOperationException("Account not found.");
        }
    }
}

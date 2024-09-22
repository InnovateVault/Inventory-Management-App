using Dapper;
using System.Data.SQLite;


namespace Inventory_Management_App;
internal class InventoryManagement
{
    private readonly string connectionString = "Data Source=InventoryDatabase.db;Version=3;";


    public async Task CreateDatabaseAsync()
    {
        using var connection = new SQLiteConnection(connectionString);
        await connection.OpenAsync();

        var createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Inventory (
                Name TEXT NOT NULL,
                Quantity INTEGER NOT NULL, 
                Price REAL NOT NULL
            );";

        await connection.ExecuteAsync(createTableQuery);
    }


}

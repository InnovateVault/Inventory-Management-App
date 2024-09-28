
using Inventory_Management_App;

internal class Program
{
    private static void Main()
    {
        InventoryManagement inventoryManagement = new();
        inventoryManagement.CreateDatabase().Wait();

        Console.WriteLine("=========================================");
        Console.WriteLine("  Welcome to the Inventory Management App  ");
        Console.WriteLine("=========================================");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Please select an option from the menu below:");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("1: Add a new item");
            Console.WriteLine("2: Restock existing items");
            Console.WriteLine("3: Record a sale");
            Console.WriteLine("4: Update product details");
            Console.WriteLine("5: View current inventory");
            Console.WriteLine("6: Delete inventory");
            Console.WriteLine("7: Exit the application");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid input. Please enter a valid number and try again.");
                continue;
            }

            if (option == 1)
            {
                Console.WriteLine("Input name of item");
                string name = Console.ReadLine();

                Console.WriteLine("Input price of item (£)");
                if (!double.TryParse(Console.ReadLine(), out double price))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

                Console.WriteLine("Input quantity of item");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

                Console.WriteLine("Input ID of item");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

                var product = new Product(name, quantity, id, price);
                inventoryManagement.AddItem(product);
                Console.WriteLine("Item Added Successfully");
            }
            else if (option == 2)
            {
                Console.WriteLine("Please enter the Item ID:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Item ID.");
                    continue;
                }

                Console.WriteLine("Please enter the amount to add:");
                if (!int.TryParse(Console.ReadLine(), out int amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the amount.");
                    continue;
                }

                try
                {
                    inventoryManagement.RestockItem(id, amount);
                    Console.WriteLine("Item Restocked Successfully");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (option == 3)
            {
                Console.WriteLine("Please enter the Item ID:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Item ID.");
                    continue;
                }

                Console.WriteLine("Please enter the amount to sell:");
                if (!int.TryParse(Console.ReadLine(), out int amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the amount.");
                    continue;
                }

                try
                {
                    inventoryManagement.SaleItem(id, amount);
                    Console.WriteLine("Item sold successfully!");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else if (option == 4)
            {
                Console.WriteLine("Please enter the Item ID:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Item ID.");
                    continue;
                }

                Console.WriteLine("What would you like to update:");
                Console.WriteLine("1: Price");
                Console.WriteLine("2: Name");


                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Item ID.");
                    continue;
                }

                string newName = null;
                decimal? newPrice = null;

                if (input == 1)
                {
                    Console.WriteLine("Whats the new price (£)");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                    {
                        Console.WriteLine("Invalid Input");
                        continue;
                    }
                    newPrice = price;
                }
                else if (input == 2)
                {
                    Console.WriteLine("What the new name");
                    newName = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("That option is not available. Please try again!");
                    continue;
                }

                try
                {
                    inventoryManagement.UpdateItem(id, newName, newPrice);
                    Console.WriteLine("Item Updated Succesfully");

                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"(Error: {ex.Message}");
                }
            }
            else if (option == 5)
            {
                Console.WriteLine("Please enter the Item ID:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Item ID.");
                    continue;
                }

                try
                {
                    Product item = inventoryManagement.ViewInventory(id);
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Quantity: {item.Quantity}");
                    Console.WriteLine($"Price (£): {item.Price}");
                    Console.WriteLine($"ID: {item.ID}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error:{ex.Message}");
                }
            }
            else if (option == 6)
            {
                Console.WriteLine("Please enter the Item ID:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Item ID.");
                    continue;
                }

                try
                {
                    inventoryManagement.DeleteItem(id);
                    Console.WriteLine("Account deleted successfully!");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else if (option == 7)
            {
                Console.WriteLine("Thank you for using the Inventory App! Have a wonderful day!");
                return;
            }
            else
            {
                Console.WriteLine("That option is not available. Please enter a valid number.");
            }
        }
    }
}
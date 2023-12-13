
using System;
using System.Text.RegularExpressions;

class SalesCalculator 
{
 
    class Customer
    {
 
        public string? FirstName { get; set; }
 
        public string? LastName { get; set; }
 
        public string? PhoneNumber { get; set; }
 
        public string? Email { get; set; }
 
        public string? Address { get; set; }
 
    }
 
    class Product
 
    {
 
        public string? Name { get; set; }
 
        public double Price { get; set; }
        
    }
 
    const double TAX_RATE = 0.04;
    
    const int Max_Customers = 3;
    
    static Product[] products = new[]
 
    {
 
        new Product { Name = "Apple", Price = 0.99 },
        
        new Product { Name = "Banana", Price = 0.59 },
        
        new Product { Name = "Cantaloupe", Price = 1.99 },

        new Product { Name = "Durian", Price = 2.99 },
        
        new Product { Name = "Eggplant", Price = 1.29 }
        
    };
 
    static Customer[] customers = new Customer[Max_Customers];
    
    static void Main(string[] args)
 
    {
        LoadCustomersFromFile();
 
        // Introduction
        
        Console.WriteLine("Welcome to the Sales Calculator Program!");
        
        Console.WriteLine( "This program calculates the total sale amount including tax for various 
        items.");
        
        
        // Customer Details
        
        
        for (int i = 0; i < Max_Customers; i++)
        
        {
 
            Console.WriteLine($"Customer {i + 1}");
            
            customers[i] = new Customer
 
            {
 
                FirstName = GetUserInput("Enter your first name: ",false),
                
                LastName = GetUserInput("Enter your last name: ", false),
                
                PhoneNumber = GetUserInput("Enter your phone number: ", false),
                
                Email = GetUserInput("Enter your email: ", true),
                
                Address = GetUserInput("Enter your address: ", false)
                
            };
 
            double totalSale = SelectProducts();
            
            double totalWithTax = totalSale * (1 + TAX_RATE);
            
            Console.WriteLine("\nCustomer Information");
            
            Console.WriteLine($"Name: {customers[i].FirstName} {customers[i].LastName}");
            
            Console.WriteLine($"Phone Number: {customers[i].PhoneNumber}");
            
            Console.WriteLine($"Email: {customers[i].Email}");
            
            Console.WriteLine($"Address: {customers[i].Address}");
            
            Console.WriteLine($"\nTotal Sale: {totalSale:C}");
            
            Console.WriteLine($"Total Sale with Tax: {totalWithTax:C}");
            
        }
        
        SaveCustomersToFile();
 
    }
    static void LoadCustomersFromFile()
    {
        if (File.Exists("customers.txt"))
        {
            string[] lines = File.ReadAllLines("customers.txt");
            for (int i = 0; i < lines.Length; i++)
            {
 
                string[] data = lines[i].Split(',');
                
                customers[i] = new Customer
                
                {
 
                    FirstName = data[0],
                    
                    LastName = data[1],
                    
                    PhoneNumber = data[2],
                    
                    Email = data[3],
                    
                    Address = data[4]
                    
                };
 
            }
 
        }
    }
    static void SaveCustomersToFile()
    {
        // Implement logic to save customers to a file
        using StreamWriter file = new("customers.txt");
        foreach (Customer customer in customers)
        {
            if (customer != null)
            {
                string line = $"{customer.FirstName},{customer.LastName},{customer.PhoneNumber},
                {customer.Email},{customer.Address}";
                file.WriteLine(line);
            }
        }
    }
    static string GetUserInput(string prompt, bool isEmail)
 
    {
 
        string input;
 
        do
 
            {
 
                Console.Write(prompt);
 
                input = Console.ReadLine() ?? "";
 
                if (isEmail && !IsValidEmail(input))
 
                {
 
                    Console.WriteLine("Invalid email. Try again.");
 
                    input = "";
 
                }
 
            } while (string.IsNullOrEmpty(input));
 
            return input;
 
        }
 
    static bool IsValidEmail(string email)
 
    {
 
         return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
 
    }
 
    static double SelectProducts()
 
    {
 
        double total = 0;
 
        Console.WriteLine("\nSelect Products (Enter 0 to stop)");
 
        for (int i = 0; i < products.Length; i++)
 
        {
 
            Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price:C}");
 
        }
 
        while (true)
 
        {
 
            Console.Write("Enter product number: ");
 
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= products.Length)
            {  
 
                if (choice == 0) break;
 
                total += products[choice - 1].Price;
 
            }
 
            else
 
            {
 
                Console.WriteLine("Invalid choice. Try again.");
 
            }
 
        } 
 
        return total;
 
    }
}
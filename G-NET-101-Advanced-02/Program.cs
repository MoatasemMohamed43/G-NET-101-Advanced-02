using System;
using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; } 
    public double Price { get; set; }
    public int Stock { get; set; }
}

internal class Program
{
    #region implementation
    //Task1: Smart Product Search
    // It lets the caller plug in ANY filter condition without modifying this method.
    static List<Product> SearchProducts(List<Product> products, Func<Product, bool> filter)
    {
        var result = new List<Product>();
        foreach (var p in products)
            if (filter(p))
                result.Add(p);
        return result;
    }

    //Task 3:Print Reports 
    // Action<Product>: built-in delegate that takes a Product and returns nothing
    static void PrintReport(List<Product> products, Action<Product> action)
    {
        foreach (var p in products)
            action(p);
    }

    //Task3: Transform Products
    // Func<Product, string>: takes a Product and returns a string
    // we convert each product into a different representation(label& summary)
    static List<string> TransformProducts(List<Product> products, Func<Product, string> transform)
    {
        var result = new List<string>();
        foreach (var p in products)
            result.Add(transform(p));
        return result;
    }

    //Task 3: Filter Products 
    // Predicate<Product>: built in delegate that takes a Product and returns bool
    static List<Product> FilterProducts(List<Product> products, Predicate<Product> condition)
    {
        var result = new List<Product>();
        foreach (var p in products)
            if (condition(p))
                result.Add(p);
        return result;
    }

    static void PrintWithStock(List<Product> list)
    {
        foreach (var p in list)
            Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})");
    }
    #endregion
    public static void Main()
    {
        List<Product> catalog = new()
        {
            new Product { Id=1, Name="Laptop", Category="Electronics", Price=1200, Stock=10 },
            new Product { Id=2, Name="Phone", Category="Electronics", Price=800, Stock=25 },
            new Product { Id=3, Name="T-Shirt", Category="Clothing", Price=30, Stock=100 },
            new Product { Id=4, Name="Jeans", Category="Clothing", Price=60, Stock=50 },
            new Product { Id=5, Name="Chocolate", Category="Food", Price=5, Stock=200 },
            new Product { Id=6, Name="Coffee Beans", Category="Food", Price=15, Stock=80 },
            new Product { Id=7, Name="C# Book", Category="Books", Price=45, Stock=30 },
            new Product { Id=8, Name="Novel", Category="Books", Price=20, Stock=60 },
            new Product { Id=9, Name="Headphones", Category="Electronics", Price=150, Stock=40 },
            new Product { Id=10, Name="Jacket", Category="Clothing", Price=120, Stock=15 }
        };

        // Task 01 
        Console.WriteLine("--- Electronics ---");
        PrintWithStock(SearchProducts(catalog, p => p.Category == "Electronics"));

        Console.WriteLine("\n--- Under $50 ---");
        PrintWithStock(SearchProducts(catalog, p => p.Price < 50));

        Console.WriteLine("\n--- In Stock ---");
        PrintWithStock(SearchProducts(catalog, p => p.Stock > 0));

        Console.WriteLine("\n--- Clothing Under $100 ---");
        PrintWithStock(SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100));

        // Task 3 
        Console.WriteLine("\n--- Short Report ---");
        PrintReport(catalog, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

        Console.WriteLine("\n--- Detailed Report ---");
        PrintReport(catalog, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));



        Console.WriteLine("\n--- Summary List ---");
        foreach (var s in TransformProducts(catalog, p => $"{p.Name} (${p.Price})"))
            Console.WriteLine(s);

        Console.WriteLine("\n--- Price Labels ---");
        var labels = TransformProducts(catalog, p => $"{p.Name}: {(p.Price > 100 ? "Expensive!" : "Affordable")}");
        foreach (var s in labels)
            Console.WriteLine(s);



        Console.WriteLine("\n--- Low-Stock Alert ---");
        foreach (var p in FilterProducts(catalog, p => p.Stock < 20))
            Console.WriteLine($"[LOW STOCK] {p.Name}: only {p.Stock} left!");
    }
}

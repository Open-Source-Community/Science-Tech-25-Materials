using System;
using System.Collections.Generic;
using System.Linq;

public struct Product
{
    public int ID { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }
    
    public Product(int id, string name, decimal price, int stock) => (ID, Name, Price, Stock) = (id, name, price, stock);
    public void Restock(int quantity) => Stock += quantity;
}

public class Warehouse : IEnumerable<Product>
{
    private List<Product> _products = new();
    public event Action<Product> ProductRestocked, LowStockWarning;
    
    public void AddProduct(Product product) => _products.Add(product);
    public void RestockProduct(int id, int quantity)
    {
        var index = _products.FindIndex(p => p.ID == id);
        if (index < 0) return;
        _products[index] = new Product(_products[index].ID, _products[index].Name, _products[index].Price, _products[index].Stock + quantity);
        ProductRestocked?.Invoke(_products[index]);
        if (_products[index].Stock <= 3) LowStockWarning?.Invoke(_products[index]);
    }
    public IEnumerator<Product> GetEnumerator() => _products.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}

public static class ProductExtensions
{
    public static decimal GetTotalValue(this IEnumerable<Product> products) => products.Sum(p => p.Price * p.Stock);
}

class Program
{
    static void Main()
    {
        Warehouse warehouse = new();
        warehouse.ProductRestocked += p => Console.WriteLine($"Restocked: {p.Name}, New Stock: {p.Stock}");
        warehouse.LowStockWarning += p => Console.WriteLine($"Low Stock: {p.Name} ({p.Stock} left!)");
        
        warehouse.AddProduct(new(1, "Falafel", 1200m, 5));
        warehouse.AddProduct(new(2, "Shawerma", 800m, 10));
        warehouse.AddProduct(new(3, "Toyota", 300m, 2));
        warehouse.AddProduct(new(4, "Batates", 150m, 8));
        
        warehouse.RestockProduct(3, 5);
        warehouse.RestockProduct(1, 3);
        
        Console.WriteLine("\nWarehouse Products:");
        foreach (var p in warehouse) Console.WriteLine($"{p.ID}: {p.Name}, {p.Price:C}, Stock: {p.Stock}");
        Console.WriteLine($"\nTotal Inventory Value: {warehouse.GetTotalValue():C}");
        
        Console.WriteLine("\nExpensive Products:");
        foreach (var p in warehouse.Where(p => p.Price > 100)) Console.WriteLine($"{p.ID}: {p.Name}, {p.Price:C}");
    }
}
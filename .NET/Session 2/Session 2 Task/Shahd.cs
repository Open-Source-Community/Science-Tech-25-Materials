using System;
using System.Collections;
using System.Collections.Generic;



public struct Product
{
    public int ID 
    { 
        get;
        set;
    }
    public string Name
    { 
        get; 
        set;
    }
    public decimal Price
    {
        get; 
        set;
    }
    public int Stock 
    { 
        get; 
        set;
    }

}

public class Warehouse : IEnumerable<Product>
{
    private List<Product> products = new List<Product>();

    public delegate void StockAlertHandler(Product product, int currentStock);
    public event StockAlertHandler ProductRestocked;
    public event StockAlertHandler LowStockWarning;

    public void AddProduct(Product product)
    {
        products.Add(product);

        if (product.Stock <= 3)
            LowStockWarning?.Invoke(product, product.Stock);
    }

    public void RestockProduct(int productId, int amount)
    {
       int index = products.FindIndex(p => p.ID == productId);
        if (index == -1) return;
        Product product = products[index];
        product.Stock += amount;
        products[index] = product;

        ProductRestocked?.Invoke(product, product.Stock);

        if (product.Stock <= 3)
            LowStockWarning?.Invoke(product, product.Stock);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in products)
            yield return product;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class Extensions
{
    public static decimal GetTotalValue(this IEnumerable<Product> products)
    {
        decimal total = 0;
        foreach (var product in products)
            total += product.Price * product.Stock;
        return total;
    }
}

class Program
{
    static void Main()
    {
        Warehouse warehouse = new Warehouse();

        warehouse.ProductRestocked += (product, stock) =>
            Console.WriteLine($"Product {product.Name} restocked. New stock: {stock}");

        warehouse.LowStockWarning += (product, stock) =>
            Console.WriteLine($"LOW STOCK ALERT: {product.Name} - Current stock: {stock}");

        warehouse.AddProduct(new Product { ID = 1, Name = "Laptop", Price = 1200m, Stock = 5 });
        warehouse.AddProduct(new Product { ID = 2, Name = "Phone", Price = 2500m, Stock = 2 }); 

        warehouse.RestockProduct(2, 1); 

        Console.WriteLine("\nAll Products:");
        foreach (var product in warehouse)
        {
            Console.WriteLine($"ID: {product.ID} | Name: {product.Name} | Stock: {product.Stock} | Price: {product.Price:C}");
        }

        Console.WriteLine($"\nTotal Inventory Value: {warehouse.GetTotalValue():C}");

        Console.WriteLine("\nExpensive Products (Price > $100):");
        foreach (var product in GetExpensiveProducts(warehouse))
        {
            Console.WriteLine($"Name: {product.Name} | Price: {product.Price:C}");
        }
    }

    private static IEnumerable<Product> GetExpensiveProducts(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            if (product.Price > 100)
                yield return product;
        }
    }
}









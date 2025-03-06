using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct Product
{
    public int ID { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        ID = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void Restock(int quantity)
    {
        Stock += quantity;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Price: {Price:C}, Stock: {Stock}";
    }
}


public delegate void StockAlertHandler(Product product);

public class Warehouse : IEnumerable<Product>
{
    private List<Product> products = new List<Product>();

    public event StockAlertHandler ProductRestocked;
    public event StockAlertHandler LowStockWarning;

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void RestockProduct(int productId, int quantity)
    {
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].ID == productId)
            {
                products[i].Restock(quantity);
                ProductRestocked?.Invoke(products[i]);
                if (products[i].Stock <= 3)
                {
                    LowStockWarning?.Invoke(products[i]);
                }
                break;
            }
        }
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in products)
        {
            yield return product;
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


public static class WarehouseExtensions
{
    public static decimal GetTotalValue(this IEnumerable<Product> products)
    {
        return products.Sum(p => p.Price * p.Stock);
    }
}


class Program
{
    static void Main()
    {
        Warehouse warehouse = new Warehouse();

        
        warehouse.ProductRestocked += (product) => Console.WriteLine($"[Restock] {product.Name} stock updated to {product.Stock}");
        warehouse.LowStockWarning += (product) => Console.WriteLine($"[Warning] {product.Name} stock is low!");

       
        warehouse.AddProduct(new Product(1, "Laptop", 1200m, 5));
        warehouse.AddProduct(new Product(2, "Mouse", 25m, 2));
        warehouse.AddProduct(new Product(3, "Keyboard", 75m, 10));
        warehouse.AddProduct(new Product(4, "Monitor", 300m, 3));

        
        Console.WriteLine("\nWarehouse Inventory:");
        foreach (var product in warehouse)
        {
            Console.WriteLine(product);
        }

      
        warehouse.RestockProduct(2, 5); 
        warehouse.RestockProduct(4, 2); 

     
        Console.WriteLine($"\nTotal Inventory Value: {warehouse.GetTotalValue():C}");

        Console.WriteLine("\nExpensive Products:");
        foreach (var product in warehouse)
        {
            if (product.Price > 100)
                Console.WriteLine(product);
        }
    }
}

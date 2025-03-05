using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
public delegate bool StockAlertHandler();
public struct Product
{
    int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int stock { get; set; }

    public Product(int id ,string name,decimal pric,int st)
    {
        Id = id;
        Name = name;
        Price = pric;
        stock = st;

    }
    public override string ToString()
    {
        return $"product  Name: {Name}, Price: ${Price}";
    }

}
public class Warehouse :IEnumerable<Product>
{
    private List<Product>procollections= new List<Product>();
    public List<Product> getlist() {  return procollections; }
    public event Action<string> ProductRestocked;
    public event Action<string> LowStockWarning;
    public void AddProduct(Product product)
    {
        procollections.Add(product);
    }
    public void RestockProduct(Product product,int q)
    {
        ProductRestocked.Invoke("stock increased");
        product.stock+=q;
    }

    public IEnumerator<Product> GetEnumerator()
    {
        
        foreach(var p  in procollections) {  yield return p; }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Product> GetExpensiveProducts()
    {
        foreach (var product in procollections)
        {
            if (product.Price > 100)
            {
                yield return product;
            }
        }
    }

}

internal static class Exten
{
    public static int  GetTotalValue(this List<Product> products)
    {
        int total = 0;
        foreach (var p in products)
        {
            total += (int)(p.Price * p.stock);
            
        }
        return total;
    }
}
namespace task_session2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Warehouse warehouse = new Warehouse();
            Product product = new Product(1,"lap1",1200,5);
            Product product1 = new Product(2, "lap2", 1500, 10);
            Product product2 = new Product(3, "lap3", 1400, 67);
            warehouse.AddProduct(product1);
            warehouse.AddProduct(product2);
            warehouse.AddProduct(product);
            warehouse.RestockProduct(product2, 10);
            foreach(var p in warehouse.getlist())
            {
                Console.WriteLine($"product name {p.Name}");
                Console.WriteLine($"product price {p.Price}");
            }
            Console.WriteLine(warehouse.getlist().GetTotalValue());

        }
    }
}

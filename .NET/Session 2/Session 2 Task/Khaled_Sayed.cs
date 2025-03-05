using System.Collections; 
class Program
{
    // warehouse inventory system
    private static void Main(string[] args)
    {
        Warehouse w = new Warehouse();

        w.ProductRestocked += (Product p) => { Console.WriteLine($"Product with id = {p.Id} has been restocked"); };
        w.LowStockWarning += (Product p) => { Console.WriteLine($"Product with id = {p.Id} has stock < 4"); };

        w.AddProduct(new Product {Id = 1 , Price = 200, Stock = 6});
        w.AddProduct(new Product {Id = 2 , Price = 120, Stock = 3});
        w.AddProduct(new Product {Id = 3 , Price = 68, Stock = 2});
        w.AddProduct(new Product {Id = 4 , Price = 45, Stock = 8});

        w.RestockProduct(new Product { Id = 1, Price = 200, Stock = 3 });
        w.RestockProduct(new Product {Id = 2 , Price = 120, Stock = 2});
        w.RestockProduct(new Product {Id = 4 , Price = 45, Stock = 4});


        foreach (var p in w) 
            Console.WriteLine(p);
         
        Console.WriteLine(w .GetTotalValue());

        Console.WriteLine(string.Join(", ", w.Where(x => x.Price > 100)));
    }   
} 
public delegate void StockAlertHandler(Product product); 
class Warehouse : IEnumerable<Product>
{
    public event StockAlertHandler ProductRestocked;
    public event StockAlertHandler LowStockWarning;

    private List<Product> products = new List<Product> ();
    public void AddProduct(Product product)
    {
        int idx = products.FindIndex(p => p.Id == product.Id);
        if (idx < 0) 
            products.Add(product);
        else // update
            products[idx] = product;
    }
    public void RestockProduct(Product product)
    {
        int idx = products.FindIndex(p => p.Id == product.Id);
        if(idx < 0)
            return;

        if (ProductRestocked is not null)
            ProductRestocked(product);
        
        products[idx] = new Product { Id = products[idx].Id, Name = products[idx].Name, Price = products[idx].Price, Stock = products[idx].Stock + product.Stock };
    }
    public IEnumerator<Product> GetEnumerator() 
    {
        foreach (var p in products)
        { 
            yield return p;
        }
    }  
    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (var p in products)
        { 
            yield return p; 
        }
    }
} 
public struct Product
{
    public int Id;
    public string Name;
    public decimal Price;
    public int Stock;
    public override string ToString()
    {
        return $"{{Id = {Id}, Price = {Price}, Stock = {Stock}}}";
    }
}
public static class Extentions
{
    public static decimal GetTotalValue(this IEnumerable<Product> products)
    {
        return products.Sum(x => x.Price * x.Stock);
    }
}
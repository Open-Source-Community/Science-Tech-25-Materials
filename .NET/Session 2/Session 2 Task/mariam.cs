using System;
using System.Collections;

public struct Product
{
	private int id;
	private string name;
	private decimal price;
	private int stock;
	public Product(int id, string name, decimal price, int stock)
	{
		this.id = id;
		this.name = name;
		this.price = price;
		this.stock = stock;
	}

	public int ID { get { return id; } set { id = value; } }
	public string Name { get { return name; } set { name = value; } }
	public decimal Price { get { return price; } set { price = value; } }
	public int Stock { get { return stock; } set { stock = value; } }
}

public class Warehouse : IEnumerable 
{
	public delegate void stockAlertHndeler();
	public List<Product> product { get; set; } = new();
	public event stockAlertHndeler ProductRestocked, LowStockWarning;

	public IEnumerator<Product> GetEnumerator()
	{
		foreach(var ele in product)
		{
			yield return ele;
		}
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}


	public void AddProduct(Product pd)
	{
		product.Add(pd);
		Console.WriteLine("product added");
		if (pd.Stock <= 3)
		{
			LowStockWarning?.Invoke();
		}

	}

	public void RestockProduct(int id, int amount)
	{
		var index = product.FindIndex(p => p.ID == id);

		product[index] = new Product(product[index].ID, product[index].Name, product[index].Price, product[index].Stock + amount);
		ProductRestocked?.Invoke();
		if (product[index].Stock <= 3)
		{
			LowStockWarning?.Invoke();
		}

	}
	

	
}

public static class Extension
{
	public static int GetTotalValue(this IEnumerable<Product> product)
	{
		int total = 0;
		foreach (var pd in product)
		{
			total += (int)(pd.Price * pd.Stock);

		}
		return total;
	}
}

namespace taskdotnet1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Warehouse warehouse = new Warehouse();

			warehouse.AddProduct(new Product { ID = 1, Name = "ghgc", Price = 120, Stock = 15 });
			warehouse.AddProduct(new Product { ID = 2, Name = "gjhfh", Price = 20m, Stock = 2 });

			warehouse.RestockProduct(2, 1);

			Console.WriteLine("\nAll Products:");
			foreach (var product in warehouse.product)
			{
				Console.WriteLine($"ID: {product.ID}  Name: {product.Name} Stock: {product.Stock}  Price: {product.Price:C}");
			}

			Console.WriteLine($"\nTotal Inventory Value: {warehouse.product.GetTotalValue():C}");

			Console.WriteLine("\nExpensive Products (Price");
			foreach (var product in GetExpensiveProducts(warehouse.product))
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
}

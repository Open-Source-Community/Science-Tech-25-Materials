using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OSC_Task.Warehouse;

namespace OSC_Task
{
    public delegate void StockAlertHandler(Product product, int oldStock);

    public struct Product
    {
        int id;
        string name;
        decimal price;
        int stock;


        public Product(int id, string name, decimal price, int stock)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.stock = stock;

        }
        public string Name { get { return name; } set { name = value; } }
        public decimal Price { get { return price; } set { price = value; } }
        public int Stock { get { return stock; } set { stock = value; } }

        public static bool operator ==(Product a, Product b)
        {
            return a.id == b.id;
        }
        public static bool operator !=(Product a, Product b)
        {
            return a.id != b.id;

        }
        public override string ToString()

        {
            Console.WriteLine("-------------------");
            return $"Product Name: {this.name}\n Product Id: {this.id}\n Product Price: {this.price}\nProduct Stock: {this.Stock}\n ------------ \n\n";

        }

    };

    class Warehouse : IEnumerable<Product>
    {
        public event StockAlertHandler OnStockIncreased;
        public event StockAlertHandler OnStockLessThanThree;

        public List<Product> products = new List<Product>();


        public void AddProduct(Product product)// extension
        {
            products.Add(product);
            CheckStockNumber(product);
        }

        private void CheckStockNumber(Product product)
        {
            if (product.Stock >= 3)
                OnStockIncreased?.Invoke(product, product.Stock);
            else
                OnStockLessThanThree?.Invoke(product, product.Stock);
        }

        public void reStockProduct(Product product, int value)
        {

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i] == product)
                {
                    Product item = products[i];
                    item.Stock += value;
                    products[i] = item;
                    CheckStockNumber(item);

                    break;
                }
            }
        }

        public void printAllProduct()
        {

            foreach (Product product in products)
            {
                string s = product.ToString();
                Console.WriteLine(s);

            }
        }
        public IEnumerator<Product> GetEnumerator()
        {
            foreach (var item in products)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public static class WarehouseExtensions
    {

        public static decimal GetTotalValue(this IEnumerable<Product> products)
        {
            return ((decimal)products.Sum(s => s.Stock * s.Price));
        }

    }


    class Program
    {
        static void Main(string[] args)
        {

            Warehouse warehouse = new Warehouse();
            warehouse.OnStockIncreased += Item_OnStockIncreased;
            warehouse.OnStockLessThanThree += Item_OnStockLessThanThree;
            warehouse.AddProduct(new Product(1, "Laptop", 1200.50m, 2));
            warehouse.AddProduct(new Product(2, "Mouse", 25.75m, 5));
            warehouse.AddProduct(new Product(3, "Keyboard", 45.30m, 3));
            warehouse.AddProduct(new Product(4, "PC", 450.30m, 5));


            warehouse.printAllProduct();
            Console.WriteLine(warehouse);
            Console.WriteLine(warehouse.GetTotalValue());


            warehouse.reStockProduct(warehouse.products[0], 5);

            foreach (var item in warehouse)
            {
                if (item.Price > 100)
                {
                    Console.WriteLine(item);

                }
            }




            Console.ReadKey();


        }
    
    

    static void Item_OnStockLessThanThree(Product product, int old)
        {
            if (product.Stock < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Product Stock is less than 3: {product.Stock}\n");
                Console.ResetColor();
            }
        }

        static void Item_OnStockIncreased(Product product, int old)
        {
            if (product.Stock > old)
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Product Stock: {product.Stock}\n");
            Console.ResetColor();

        }
    }
}
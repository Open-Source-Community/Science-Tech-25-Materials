using System;

namespace osc_.net_task1 {

    public struct Product {
        private int id;
        private string name;
        private decimal price;
        private int stock;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public void restock(int newStock) {
            this.stock += newStock;
        }

        public Product(int id,string name,decimal price,int stock) {
            this.id = id;
            this.name = name;
            this.price = price;
            this.stock=stock;
        }
    }

    class Program {
        static void Main() {
            Warehouse Warehouse1 = new Warehouse();
            Warehouse1.AddProduct(new Product(0,"product one",20,2));
            Warehouse1.AddProduct(new Product(1,"product two", 30, 3));
            Warehouse1.AddProduct(new Product(2,"product three", 140,4));
            Warehouse1.AddProduct(new Product(3,"product four", 150,5));
            Warehouse1.ProductRestocked += Warehouse1.ProductIncreaseNotification;
            Warehouse1.LowStockWarning += Warehouse1.LowStockNotification;
            Warehouse1.RestockProduct(2, 7);
            Warehouse1.LowStockIdentification();
            Console.WriteLine("\nID\t\t\tName\t\t\tPrice\t\t\tStock");
            foreach (var item in Warehouse1) {
                Console.WriteLine(item.Id+"\t\t\t"+item.Name+"\t\t"+item.Price+"\t\t\t"+item.Stock);
            }
            Console.WriteLine("\n\nTotal Value:" + Warehouse1.GetTotalVale());
            Console.WriteLine("\n\n--------------------------------------Expensive Products ------------------------------");
            Console.WriteLine("ID\t\t\tName\t\t\tPrice\t\t\tStock");
            foreach (var item in Warehouse1.GetExpensiveProducts())
            {
                Console.WriteLine(item.Id + "\t\t\t" + item.Name + "\t\t" + item.Price + "\t\t\t" + item.Stock);
            }
        }
    }
}
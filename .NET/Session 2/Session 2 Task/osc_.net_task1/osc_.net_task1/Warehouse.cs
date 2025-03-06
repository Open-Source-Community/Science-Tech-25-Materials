using osc_.net_task1;
using System;
using System.Collections;
using System.Collections.Generic;

namespace osc_.net_task1
{
    public delegate void StockAlertHandler(int id,int newStock);
    class Warehouse : IEnumerable<Product>
    {
        public event StockAlertHandler ProductRestocked;
        public event StockAlertHandler LowStockWarning;
        private List<Product> products = new List<Product>();
        public void AddProduct(Product P) {
            products.Add(P);
        }
        public void RestockProduct(int id,int newStock)
        {
            Product temp;
            for (int i = 0; i < products.Count; i++)
            {

                if (products[i].Id == id)
                {
                    temp = products[i];
                    temp.restock(newStock);
                    products[i] = temp;
                    ProductRestocked?.Invoke(id, newStock);
                    break;
                }
            }
        }
        public void LowStockIdentification()
        {
            foreach (var item in products)
            {
                if (item.Stock <= 3) {
                   LowStockWarning?.Invoke(item.Id, item.Stock);
                }
            }
        }
        public IEnumerator<Product> GetEnumerator()
        {
            foreach (var item in products)
            {
                yield return item;
            }
        }
        public IEnumerable<Product> GetExpensiveProducts()
        {
            foreach (var item in products)
            {
                if(item.Price > 100)
                   yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void LowStockNotification(int id,int stock)
        {
            Console.WriteLine("\nThe product with id (" + id + "):\nHave only " + stock+"left.");
        }
        public void ProductIncreaseNotification(int id, int stock)
        {
            Console.WriteLine("\nThe product with id (" + id + "):\nIt is amount have been increased and become " + stock);
        }
    }
}
 
static class TotalValue
{
    public static decimal GetTotalVale(this IEnumerable<Product> p)
    {
        decimal result = 0;
        foreach (var item in p)
        {
            result += item.Price * item.Stock;
        }
        return result;
    }
}

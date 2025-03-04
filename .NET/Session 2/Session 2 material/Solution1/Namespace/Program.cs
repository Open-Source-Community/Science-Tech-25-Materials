//global using System.IO;//C# 10 or gigher version ,used for defining namespace global in your project
//using static System.Console;
//using L = System.Console;

namespace Namespace
{
    using WithoutConsole;
    internal class Program
    {
        static void Main(string[] args)
        {
           //WriteLine("Hello without Console");
           // L.WriteLine("Hello With alias L");
            One.print();

        }
    }
}
namespace WithoutConsole
{
    using static System.Console;
    class One
    {
        public static void print()
        {
            WriteLine("Withoutconsole in print");
        }
    }
}
namespace Three
{
    class ThreeClass
    {
        public static void print()
        {
            Console.WriteLine("printing in line 3 ");
        }
    }
}
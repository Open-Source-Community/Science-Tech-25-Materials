using System;
using OSC.Employees;
using OSC;
using System.Runtime.InteropServices;

namespace OSC
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region ReadWrite

            //string s = Console.ReadLine();
            //int y = int.Parse(Console.ReadLine());

            //Console.WriteLine($"This is string has {s}");


            #endregion

            #region Data types

            //string s1 = "Hello";
            //string s2 = "Hello";

            //Console.WriteLine(s1.Equals(s2));

            #endregion

            #region Operators

            //&&, ||
            //&, |

            //if(false&check())
            //{
            //    Console.WriteLine("true");
            //}

            #endregion

            #region Arrays

            //int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //int[][] jaggedArray = new int[][]
            //{
            //     new int[] { 9, 10 },
            //     new int[] { 1, 2, 3, 4 },
            //     new int[] { 4, 5, 6 },
            //};

            //int[,] MulDimensinalArray = new int[,]
            //{
            //     { 2, 3 },
            //     { 4, 5 }
            //};

            #endregion

            #region Expressions

            //string s2 = null;
            //s2 = s2 ?? "Ahmed";
            //s2 = s2 ?? "Sayed";
            ////Console.WriteLine(s);
            //Console.WriteLine(s2?.ToUpper());
            //Console.WriteLine("Mahmoud");

            #endregion

            #region Loops

            //List<string> Students = new List<string> { "Mahmoud", "Khaled", "George" };

            //foreach (string student in Students)
            //{
            //    Console.WriteLine(student);
            //}

            #endregion

            #region switch

            //Regular
            //int x = 5;
            //switch (x)
            //{
            //    case 1:
            //    case 3:
            //    case 5:
            //    case 7:
            //    case 9:
            //        Console.WriteLine($"{x} is Odd");
            //        break;
            //    default:
            //        Console.WriteLine($"{x} is Even");
            //        break;
            //}

            ////switch on types
            //object o = 3;
            //switch (o)
            //{
            //    case int i:
            //        Console.WriteLine($"{i} is int");
            //        break;
            //    case string i:
            //        Console.WriteLine($" {i} is string");
            //        break;
            //}

            ////When 
            //bool b = true;
            //switch (b)
            //{
            //    case bool when b = true:
            //        Console.WriteLine($"is true");
            //        break;
            //    case bool when b = false:
            //        Console.WriteLine($" is false");
            //        break;
            //}

            //swtich Expression
            //int CardNo = int.Parse(Console.ReadLine());

            //string cardName = CardNo switch
            //{
            //    1 => "Ace",
            //    11 => "JAck",
            //    12 => "Queen",
            //    13 => "King",
            //    _ => CardNo.ToString()
            //};



            #endregion

            #region goto

            //Mahmooud:;

            //string n = null;
            //if (n == null)
            //{
            //    Console.WriteLine("null");
            //}

            //goto Mahmooud;

            #endregion

            #region Casting

            //int ni = 100;
            //long nl = ni; //implicit Casting

            //long nl1 = 100;
            //if (nl1 < Int32.MaxValue)
            //{
            //    int ni1 = (int)nl1; //Explicit Casting
            //}
            //============================================

            //int b1 = 1;
            //object o1 = b1; //Boxing (From value to Reference)

            //object o2 = 1;
            //int b2 = (int)o2; //Unboxing (From Reference to Value)

            #endregion

            #region Parsing

            //string str = "m123";
            //int n;
            //n = Convert.(str);

            //Console.WriteLine(n);
            //int.TryParse(str, out n);

            #endregion

            #region Call Method by Reference

            //int a = 1;
            //change(ref a);
            //Console.WriteLine(a);

            #endregion

            #region UpAndDownCasting

            //Animal a = new Animal("Cat", 7);
            //a.Name = "Cat1";
            //Cat cat = (Cat)a;

            //Console.WriteLine(a);


            #endregion

            #region Date
            ////Constructors

            //Date date = new Date(2000);
            //date.Year = 1999;
            //Console.WriteLine(date.Year);

            #endregion

            #region Employee
            ////Empoyee Class
            
            //Employee emp = Employee.Create(1, "Hazem");
            //Console.WriteLine(emp);

            #endregion

            #region IP Address
            ////Indexers

            //IP ip = new IP(255,255,255,0);
            //Console.WriteLine(ip[0]);


            #endregion

            #region Interface
            ////Vehicle Class

            #endregion

            #region Int Extensions
            ////Extension Methods

            //int n = 5;
            //n.IsEven();

            #endregion

            #region Generics
            //Print<int> p = new Print<int>();
            //p.print(2);
            #endregion
        }
        static bool check()
        {
            Console.WriteLine("Checking");
            return true;
        }

        static void change(ref int a)
        {
            a += 1;
        }
    } 
}

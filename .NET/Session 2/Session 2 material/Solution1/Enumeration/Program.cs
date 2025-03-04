using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Indexers
{  
    internal class Program
    {
        static void Main(string[] args)
        {
//             List<int>list = new List<int>();

//            //example with indexer 
//            IndexerExample indexer = new IndexerExample(5);
//            indexer[0] = 'A';
//            indexer[1] = 'h';
//            indexer[2] = 'm';
//            indexer[3] = 'e';
//            indexer[4] = 'd';
//            Console.WriteLine(indexer);
//            for (int i = 0; i < indexer.Length; i++)
//            {
//                Console.WriteLine(@$"The value of the {i} index 
//is {indexer[i]}");
//            }
//            foreach (var x in indexer)
//            {
//                Console.WriteLine("value with foreach " + x);
//            }

            int percentage = 25;
            percentage.betweenRange(0, 100).valuerange(0, 100);
            
           
        }

    }
    static class Helper
    {
        public static int betweenRange(this int  value, int min,int max)
        {
            return value;
        }
        public static void valuerange(this int value, int min, int max)
        {
            Console.WriteLine($"{value} {min} {max}");
        }

    }
    class FixedExample
    {
        private int length;
        private char[] word;
        public int Length { get { return length; } }
        public FixedExample(int length)
        {
            this.length = length;
            word = new char[length];
        }

        ~FixedExample() => Console.WriteLine("We are done here ");

    }
    class IndexerExample : IEnumerable
    {
        private int length;
        private char[] word;
        public int Length { get { return length; } }
        public IndexerExample(int length)
        {
            this.length = length;
            word = new char[length];
        }
        public char this[int index]
        {
            get
            {
                return word[index];
            }
            set
            {
                word[index] = value;
            }
        }
        public override string ToString()
        {
            return string.Join("", word);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in word)
            {
                yield return item;
            }
        }

    }

}

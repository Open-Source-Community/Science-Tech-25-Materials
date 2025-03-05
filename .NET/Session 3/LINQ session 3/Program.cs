using System;
using System.Collections.Generic;
using System.Linq;
using LINQ_session_3;

namespace LINQDemoProject
{
   

    // ----------------------------------------------
    // 4) Program Entry Point
    // ----------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("    C# LINQ & Functional Programming Demo  ");
            Console.WriteLine("===========================================\n");

            // Call each demo in order (comment/uncomment as needed)
            //LINQExamples.IntroductionToLINQ();
           // LINQExamples.PureVsImpureFunctions();
            //LINQExamples.ProjectionOperations();
            //LINQExamples.SortingData();
           // LINQExamples.DataPartitioning();
           // LINQExamples.Quantifiers();
            //LINQExamples.JoinOperations();
          //  LINQExamples.GenerationOperations();
          //  LINQExamples.ElementOperations();
           // LINQExamples.EqualityOperations();
            //LINQExamples.ConcatenationOperations();
            //LINQExamples.AggregateOperations();
            LINQExamples.SetOperations();

            Console.WriteLine("Demo complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}

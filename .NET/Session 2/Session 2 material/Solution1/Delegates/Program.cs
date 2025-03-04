using System;

// Declare a delegate
public delegate bool Operation(int x, int y);

public class DelegateExample
{
    public static bool check(Operation op)
    {
        return true;
    }

    public static void Subtract(int a, int b)
    {
        Console.WriteLine($"Subtraction: {a - b}");
    }

    public static void Main()
    {
        // Instantiate delegate with a method
        


    }
}
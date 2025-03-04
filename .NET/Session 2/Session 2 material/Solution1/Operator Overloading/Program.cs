using System;

public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Grade { get; set; }

    // Constructor
    public Student(int id, string name, double grade)
    {
        ID = id;
        Name = name;
        Grade = grade;
    }

    // Overload + operator
    // (For demonstration: returns a new Student with combined grade)
    public static Student operator +(Student s1, Student s2)
    {
        return new Student(
            s1.ID,                       // Keeping first student's ID
            s1.Name + " & " + s2.Name,   
            s1.Grade + s2.Grade         
        );
    }

    // Overload > operator (compare by Grade)
    public static bool operator >(Student s1, Student s2)
    {
        return s1.Grade > s2.Grade;
    }

    // Overload < operator (compare by Grade)
    public static bool operator <(Student s1, Student s2)
    {
        return s1.Grade < s2.Grade;
    }

    // Overload == operator
    public static bool operator ==(Student s1, Student s2)
    {
        // Handle null checks (to avoid NullReferenceException)
        if (ReferenceEquals(s1, null) && ReferenceEquals(s2, null))
            return true;
        if (ReferenceEquals(s1, null) || ReferenceEquals(s2, null))
            return false;

        return s1.ID == s2.ID &&
               s1.Name == s2.Name &&
               s1.Grade == s2.Grade;
    }

    // Overload != operator
    public static bool operator !=(Student s1, Student s2)
    {
        return !(s1 == s2);
    }

    // Override Equals (must do this when overloading == and !=)
    public override bool Equals(object obj)
    {
        if (!(obj is Student))
            return false;

        var other = (Student)obj;
        return this == other; 
    }

    // Override GetHashCode (must do this when overloading == and !=)
    public override int GetHashCode()
    {
        // Combine all relevant fields into a single hash
        return ID * 37 + (int)Grade * 13;
    }

    // Override ToString() for easy display
    public override string ToString()
    {
        return $"Student[ID={ID}, Name={Name}, Grade={Grade}]";
    }
}

class Program
{
    static void Main()
    {
        // Create two students
        Student s1 = new Student(1, "Alice", 85.5);
        Student s2 = new Student(2, "Bob", 90.0);

        // Operator + (Combining Students)
        Student combined = s1 + s2;
        Console.WriteLine("Combined Student: " + combined);

        // Operator > and <
        Console.WriteLine($"Is {s1.Name} > {s2.Name}? {s1 > s2}");
        Console.WriteLine($"Is {s1.Name} < {s2.Name}? {s1 < s2}");

        // Operator == and !=
        Student s3 = new Student(1, "Alice", 85.5);
        Console.WriteLine($"Is s1 == s3? {s1 == s3}");
        Console.WriteLine($"Is s1 != s2? {s1 != s2}");
    }
}


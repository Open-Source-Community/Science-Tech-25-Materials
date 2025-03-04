using System;

struct Student
{
   
    private int id;
    private string name;
    private double grade;

    
 
    public Student(int id, string name, double grade)
    {
        this.id = id;
        this.name = name;
        this.grade = grade;

    }


    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public double Grade
    {
        get { return grade; }
        set
        {
            grade = value;
        }
    }

    // Method
    public void DisplayInfo()
    {
        Console.WriteLine($"Student ID: {ID}, Name: {Name}, Grade: {Grade}");
    }

    // Indexer
    private double[] scores = new double[5];

    public double this[int index]
    {
        get { return scores[index]; }
        set { scores[index] = value; }
    }

    // Operator Overloading (Comparing Students by Grade)

    public static bool operator >(Student s1, Student s2)
    {
        return s1.Grade > s2.Grade;
    }

    public static bool operator <(Student s1, Student s2)
    {
        return s1.Grade < s2.Grade;
    }
    public static bool operator ==(Student s1, Student s2)
    {
        return s1.grade == s2.grade;
    }
    public static bool operator !=(Student s1, Student s2)
    {
        return s1.grade != s2.grade;
    }

}

class Program
{
    static void Main()
    {
        // Create Student instance
        Student student1 = new Student(101, "John Doe", 85.5);

        // Access properties and method
        student1.DisplayInfo();

        student1.Grade = 90.0;

        // Using Indexer
        student1[0] = 95.0;
        Console.WriteLine($"First Subject Score: {student1[0]}");

        // Operator overloading comparison
        Student student2 = new Student(102, "Jane Smith", 88.0);
        Console.WriteLine(student1 > student2 ? "John has a higher grade." : "Jane has a higher grade.");
    }
}

namespace OSC.Employees;

public class Employee
{
    private int id;
    private string name;

    private Employee() //private default constructor
    {
    }

    private Employee(int id, string name) //private constructor
    {
        this.id = id;
        this.name = name;
    }

    public static Employee Create(int id, string name) //return new instance
    {
        return new Employee(id, name);
    }

    public override string ToString() //Overriding to return what prints when printing the object by it's name
    {
        return name;
    }
}
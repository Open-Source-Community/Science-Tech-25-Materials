namespace OSC;

public class Animal
{
    private string name;
    private int age;

    public string Name //Proprety
    {
        get { return name; }
        set { name = value; }
    }

    public int Age //Proprety
    {
        get { return age; }
        set { age = value; }
    }

    public Animal(string name, int age) 
    {
        this.name = name;
        this.age = age;
    }

    public virtual void Eat()
    {
        Console.WriteLine("The Animal is eating!");
    }
}

class Cat : Animal //Inheritance
{
    public Cat(string name, int age) : base(name, age)
    {
    }

    public void Sound()
    {
        Console.WriteLine("Meow!");
    }

    public override void Eat() //Override
    {
        base.Eat();
        Console.WriteLine("Meow!");
    }
}
namespace OSC;

public abstract class Vehicle
{
    private string model;
    private string color;

    public Vehicle(string model, string color)
    {
        this.model = model;
        this.color = color;
    }
}

class Car : Vehicle
{
    public Car(string model, string color) : base(model, color)
    {
    }
}

interface ILoading
{
    void Load();
}

class Loader: Vehicle, ILoading
{
    public Loader(string model, string color) : base(model, color)
    {

    }

    public void Load()
    {
        Console.WriteLine("Loading");
    }
}
var tesla = new Car("Tesla Model S")
{
    MaxSpeed = 250,
    NumPassengers = 5
};

var harley = new Bike("Harley Davidson")
{
    MaxSpeed = 180,
    NumPassengers = 2
};


tesla.Start();
tesla.Accelerate(50);
tesla.Stop();
harley.Start();
harley.Accelerate(40);
harley.Stop();

// Upgrade engines
tesla.UpgradeEngine(EngineType.Premium);
harley.UpgradeEngine(EngineType.Pro);

// Display vehicle information
tesla.ShowInfo();
harley.ShowInfo();

public enum EngineType
{
    Standard,
    Premium,
    Pro,
}

public class Vehicle
{
    public string Model { get; set; }
    public string Type { get; set; }
    public int NumPassengers { get; set; } = 5;
    public int MaxSpeed { get; set; } = 220;
    public bool IsRunning { get; private set; } = false;
    public int Speed { get; private set; } = 0;
    public EngineType Engine { get; set; } = EngineType.Standard;

    public Vehicle(string model, string type)
    {
        Model = model;
        Type = type;
    }

    public void EditInfo(string model, string type)
    {
        Model = model;
        Type = type;
    }

    public virtual void ShowInfo()
    {
        Console.WriteLine("This is a vehicle: " + Model);
    }

    public void Start()
    {
        if (!IsRunning)
        {
            IsRunning = true;
            Console.WriteLine($"{Model} is starting.");
        }
    }

    public void Stop()
    {
        if (IsRunning)
        {
            IsRunning = false;
            Speed = 0;
            Console.WriteLine($"{Model} is stopping.");
        }
    }

    public void Accelerate(int amount)
    {
        if (IsRunning && Speed + amount <= MaxSpeed)
        {
            Speed += amount;
            Console.WriteLine($"{Model} ({Type}) is accelerating.");
        }
    }

    public void Decelerate(int amount)
    {
        if (IsRunning && amount <= Speed)
        {
            Speed -= amount;
            Console.WriteLine($"{Model} is decelerating.");
        }
    }
}

public class UpgradeableVehicle : Vehicle
{
    public UpgradeableVehicle(string model, string type) : base(model, type) { }

    public void UpgradeEngine(EngineType engine)
    {
        if (engine != Engine)
        {
            Engine = engine;
            Console.WriteLine($"{Model} ({Type}) engine is being upgraded.");
        }
    }
}

public class Car : UpgradeableVehicle
{
    public Car(string model) : base(model, "Car") { }

    public override void ShowInfo()
    {
        Console.WriteLine("This is a car: " + Model);
    }
}

public class Bike : UpgradeableVehicle
{
    public Bike(string model) : base(model, "Bike") { }

    public override void ShowInfo()
    {
        Console.WriteLine("This is a bike: " + Model);
    }
}

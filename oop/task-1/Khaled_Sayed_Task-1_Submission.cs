public class Program
{
    public static void Main(string[] args)
    {
        Car car = new Car("Tesla Model S");
        car.Start();
        car.Accelerate();
        car.Stop();
        car.Upgrade();

        Console.WriteLine("\n---------\n");

        Bike bike = new Bike("Harley Davidson");
        bike.Start();
        bike.Accelerate();
        bike.Stop();
        bike.Upgrade(); 
    }
    public interface IDrivable
    {
        public void Start();
        public void Stop();
        public void Accelerate();
    }
    public interface IUpgradable
    {
        public void Upgrade();
    }

    public abstract class Vehicle  : IDrivable
    {
        protected string Type;
        protected string Model;
        protected bool On;
        public Vehicle(string type, string model)
        {
            Type = type; 
            Model = model;
            On = false;
        }

        virtual public void Start()
        {
            On = true;
            Console.WriteLine($"{Model} is starting.");
        }
        virtual public void Stop()
        {
            if (!On)
            {
                Console.WriteLine($"{Model} is off.");
                return;
            }
            On = false;
            Console.WriteLine($"{Model} is stopping.");
        }
        virtual public void Accelerate()
        { 
            if(!On)
            {
                Console.WriteLine($"{Model} ({Type}) is off.");
                return;
            }
            Console.WriteLine($"{Model} ({Type}) is accelerating.");
        }

        public abstract void DisplayInfo();
    }

    public class Car : Vehicle, IUpgradable
    {
        public Car(string model) : base(nameof(Car), model) { }


        public void Upgrade()
        { 
            if(On)
            {
                Console.WriteLine($"{Model} ({Type}) engine can't be upgraded while it is on!");
                return;
            }
            Console.WriteLine($"{Model} ({Type}) engine is being upgraded.");
        }

        override
        public void DisplayInfo() 
        { 
            Console.WriteLine($"This is a car: {Model}");
        }
    }

    public class Bike : Vehicle, IUpgradable
    {
        public Bike(string model) : base(nameof(Bike), model) { }
           
        public void Upgrade()
        {  
            if(On)
            {
                Console.WriteLine($"{Model} ({Type}) engine can't be upgraded while it is on!");
                return;
            }
            Console.WriteLine($"{Model} ({Type}) engine is being upgraded.");
        }

        override
        public void DisplayInfo()
        { 
            Console.WriteLine($"This is a bike: {Model}");
        }
    }
}

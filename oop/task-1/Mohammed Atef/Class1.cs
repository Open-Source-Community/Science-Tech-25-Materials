using System;
using System.ComponentModel;
using System.Transactions;

interface Idreivable
{
    public void startingTheVehicle();
    public void accelaratingTheVehicle();
    public void stopingTheVehicle();
}
interface IUpgradeable
{
    void UpgradeEngine();
}
public abstract class Vehicle :Idreivable
{
	private string model;
	private string name;
	private string type;
	public Vehicle()
	{
		this.type = "";
		this.model = "";
		this.name = "";
	}
	public Vehicle(string model, string name, string type)
    {
        this.model = model;
        this.name = name;
        this.type = type;
    }
	public string Model { set { model = value; } get { return this.model; } }
	public string Name { set { name = value; } get { return this.name; } }
	public string Type { set { type = value; } get { return this.type; } }

	//the original method of displaying the vehicle data
	public void displayingInformation()
	{
        Console.WriteLine(@$"The name of the Vehicle is {this.name}

The type of the Vehicle is {this.type}
The model of the Vehicle is {this.model}
");
    }	
	public void startingTheVehicle()
	{
        Console.WriteLine($"{this.model} {this.name} is starting");
    }
    public void accelaratingTheVehicle()
    {
        Console.WriteLine($"{this.model} {this.type} {this.name} is accelarating");
    }
    public void stopingTheVehicle()
    {
        Console.WriteLine($"{this.model}  {this.name} is stoping");
    }
}
public  class UpgradableVehicle : Vehicle, IUpgradeable
{
    public UpgradableVehicle() : base() { }
    public UpgradableVehicle(string model, string type, string name) : base(model, type, name) { }

    public   void UpgradeEngine()
    {
        Console.WriteLine($"{Model}  {Type}  {Name} Engine is being upgraded");
    }
}
public class Car  : UpgradableVehicle
{

    public Car(string model, string type, string name) : base(model, "Car", name) { }

}
public class Bike : UpgradableVehicle
{
    public Bike(string model, string type, string name) : base(model, "Bike", name) { }
   
}


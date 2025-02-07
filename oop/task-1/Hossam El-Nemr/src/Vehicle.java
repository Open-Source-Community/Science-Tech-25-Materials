public abstract class Vehicle {
    String name;
    public Vehicle(String name) {
        this.name = name;
    }
    public abstract void displayInfo();
    public void start()
    {
        System.out.println("Vehicle is starting");
    }
    public void stop()
    {
        System.out.println("Vehicle is stopping");
    }
    public void accelerate()
    {
        System.out.println("Vehicle is accelerating");
    }
}
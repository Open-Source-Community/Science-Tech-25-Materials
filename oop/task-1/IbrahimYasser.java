interface Upgradeable{
    void upgradeEngine();
}
abstract class Vehicle{
    protected String model;
    public Vehicle(String model){
        this.model = model;
    }
    public void start(){
        System.out.println(this.model + " is starting.");
    }
    abstract public void accelerate();
    public void stop(){
        System.out.println(this.model + " is stopping.");
    }
    abstract public void displayInfo();
}
class Car extends Vehicle implements Upgradeable{
    public Car(String model){
        super(model);
    }
    public void accelerate(){
        System.out.println(this.model + " (Car) is accelerating.");
    }
    public void displayInfo(){
        System.out.println("This is a car: " + this.model);
    }
    public void upgradeEngine(){
        System.out.println(this.model + " (Car) is being upgraded.");
    }
}
class Bike extends Vehicle implements Upgradeable{
    public Bike(String model){
        super(model);
    }
    public void accelerate(){
        System.out.println(this.model + " (Bike) is accelerating.");
    }
    public void displayInfo(){
        System.out.println("This is a bike: " + this.model);
    }
    public void upgradeEngine(){
        System.out.println(this.model + " (Bike) is being upgraded.");
    }
}
public class IbrahimYasser{
    public static void main(String[] args) {
        // Create instances of Car and Bike
        Car car = new Car("Tesla Model S");
        Bike bike = new Bike("Harley Davidson");

        // Demonstrate basic driving behavior
        car.start();
        car.accelerate();
        car.stop();

        bike.start();
        bike.accelerate();
        bike.stop();

        // Demonstrate upgrade behavior
         car.upgradeEngine();
        bike.upgradeEngine();

        // Display vehicle information
        car.displayInfo();
        bike.displayInfo();
    }
}

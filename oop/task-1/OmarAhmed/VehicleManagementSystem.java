public class VehicleManagementSystem {
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
interface Drivable {
    void start();
    void stop();
    void accelerate();
}

interface Upgradeable {
    void upgradeEngine();
}

abstract class Vehicle implements Drivable {
    protected String model;

    public Vehicle(String model) {
        this.model = model;
    }

    public void start() {
        System.out.println(model + " is starting.");
    }

    public void stop() {
        System.out.println(model + " is stopping.");
    }

    public abstract void displayInfo();
}

class Car extends Vehicle implements Upgradeable {
    public Car(String model) {
        super(model);
    }

    public void accelerate() {
        System.out.println(model + " (Car) is accelerating.");
    }

    public void upgradeEngine() {
        System.out.println(model + " (Car) engine is being upgraded.");
    }

    public void displayInfo() {
        System.out.println("This is a car: " + model);
    }
}

class Bike extends Vehicle implements Upgradeable {
    public Bike(String model) {
        super(model);
    }

    public void accelerate() {
        System.out.println(model + " (Bike) is accelerating.");
    }

    public void upgradeEngine() {
        System.out.println(model + " (Bike) engine is being upgraded.");
    }

    public void displayInfo() {
        System.out.println("This is a bike: " + model);
    }
}
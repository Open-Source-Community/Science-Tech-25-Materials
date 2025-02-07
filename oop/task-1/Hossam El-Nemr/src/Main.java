import java.util.*;
public class Main {
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
        car.upgrade();
        bike.upgrade();
        // Display vehicle information
        car.displayInfo();
        bike.displayInfo();
    }
}
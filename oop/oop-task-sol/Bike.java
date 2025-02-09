// Represents a bike, which is a type of vehicle
class Bike extends Vehicle implements Upgradable {
    public Bike(String model) {
        super(model);
    }
    @Override
    public void accelerate() {
        System.out.println(model + " (Bike) is accelerating.");
    }
    @Override
    public void upgradeEngine() {
        System.out.println(model + " (Bike) engine is being upgraded.");
    }
    @Override
    public void displayInfo() {
        System.out.println("This is a bike: " + model);
    }
}
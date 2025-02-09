
abstract class Vehicle implements Drivable {

    protected String model;

    public Vehicle(String model) {
        this.model = model;
    }
    @Override
    public void start() {
        System.out.println(model + " is starting.");
    }
    @Override
    public void stop() {
        System.out.println(model + " is stopping.");
    }
    // Abstract method to display vehicle information
    public abstract void displayInfo();
}
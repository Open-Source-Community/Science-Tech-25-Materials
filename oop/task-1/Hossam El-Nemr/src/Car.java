public class Car extends Vehicle implements Upgradeable{
    public Car(String name) {
        super(name);
    }
    @Override
    public void displayInfo() {
        System.out.println("This is a car: " + name);
    }
    @Override
    public void upgrade() {
        System.out.println(name + " (Car) engine is being upgraded");
    }
}
public class Bike extends Vehicle implements Upgradeable{
    public Bike(String name) {
        super(name);
    }
    @Override
    public void displayInfo() {
        System.out.println("This is a Bike: " + name);
    }
    @Override
    public void upgrade() {
        System.out.println(name + " (Bike) engine is being upgraded");
    }
}
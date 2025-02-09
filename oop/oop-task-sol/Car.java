class Car extends Vehicle implements Upgradable {
    public Car(String model) {
        super(model);
    }
    @Override
    public void accelerate() {
        System.out.println(model + " (Car) is accelerating.");
    }
    @Override
    public void upgradeEngine() {
        System.out.println(model + " (Car) engine is being upgraded.");
    }
    @Override
    public void displayInfo() {
        System.out.println("This is a car: " + model);
    }
}
class Car extends Vehicle implements Upgrade {
    public Car(String modelName) {
        super(modelName, "Car");
    }
    @Override
    public void upgradeEngine() {
        System.out.println(modelName + " engine upgraded.");
    }

    @Override
    public void displayInfo() {
        System.out.println("This is a car of type " + modelName);
    }
}
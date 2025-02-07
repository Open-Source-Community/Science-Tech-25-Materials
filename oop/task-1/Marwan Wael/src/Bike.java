class Bike extends Vehicle implements Upgrade {
    public Bike(String modelName) {
        super(modelName, "Bike");
    }

    @Override
    public void upgradeEngine() {
        System.out.println(modelName + " engine upgraded.");
    }

    @Override
    public void displayInfo() {
        System.out.println("This is a bike of type " + modelName);
    }
}
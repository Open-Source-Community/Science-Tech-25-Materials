public abstract class Vehicle implements Drivable
{
    protected String modelName;
    protected String type;

    public Vehicle(String modelName, String type) {
        this.modelName = modelName;
        this.type = type;
    }

    public String getModelName() {
        return modelName;
    }

    public void setModelName(String modelName) {
        this.modelName = modelName;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    @Override
    public void start() {
        System.out.println("Start " + modelName);
    }

    @Override
    public void acc() {
        System.out.println("Accelerate " +  modelName);
    }

    @Override
    public void stop() {
        System.out.println("Stop" + modelName);
    }

    public abstract void displayInfo();
}


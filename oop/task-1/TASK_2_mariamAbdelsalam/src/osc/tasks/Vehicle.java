package osc.tasks;

public abstract class Vehicle implements Driveable {
    protected String type;
    protected String model;

    public Vehicle( String model) {
        this.model = model;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }


    public abstract void displayInfo();

    @Override
    public void start(){
        System.out.println(this.model +" is starting");
    }

    @Override
    public void accelerate() {
        System.out.println(this.model+"  ("+this.type +") is accelerating");
    }

    @Override
    public void stop(){
        System.out.println(this.model +" is starting");
    }
}

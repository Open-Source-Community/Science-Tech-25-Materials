public abstract class Vehicle implements Drivable{
   private String model;
    private String name;
    private String type;
    public Vehicle(String name, String model, String type) {
        this.model = model;
        this.name = name;
        this.type = type;
    }

    public String getModel() {
        return model;
    }
    public void setModel(String model) {
        this.model = model;
    }
    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public String getType() {
        return type;
    }
    public void setType(String type) {
        this.type = type;
    }


    public abstract String displayInfo();


    @Override
    public void startVehicle() {
        System.out.println(this.getName() + " " + this.getModel()+ " (" + this.getType() + ") is starting" );
    }

    @Override
    public void stopVehicle() {

        System.out.println(this.getName() + " " + this.getModel()+ " (" + this.getType() + ") is stopping" );
    }

    @Override
    public void accelerateVehicle() {

        System.out.println(this.getName() + " " + this.getModel()+ " (" + this.getType() + ") is accelerating" );
    }




}

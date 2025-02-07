public abstract class Vehicle implements Drivable{
    private String modelname;
    private   String type;
    public Vehicle(String modelname, String type){
        this.modelname = modelname;
        this.type = type;
    }
    public String getModelname(){
        return modelname;
    }


    public abstract void displayinfo();

}

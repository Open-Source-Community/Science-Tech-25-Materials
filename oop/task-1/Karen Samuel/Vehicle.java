public abstract class Vehicle {
    String model;
    String name;
    String Type;

    public Vehicle( String type,String model, String name) {
        this.model = model;
        this.name = name;
        Type = type;
    }

    abstract void display_info();
    void startvehicle(){

        System.out.println(name+" "+model+" is starting");
    }
    void stopvehicle(){
        System.out.println(name+" "+model+" is stopping");
    }
    void accelratevehicle(){
        System.out.println(name+" "+model+" ("+Type+")"+" is accelerating");
    }
}

package osc.tasks;

public class Bike extends Vehicle implements Ubgradeable {

    public Bike( String model) {
        super( model);
        this.type="Bike";
    }

    @Override
    public void displayInfo(){
        System.out.println("This is a bike \n model: "+ this.getModel()+"\n type: "+this.getType());
    }
    @Override
    public  void upgradeEngine(){
        System.out.println(this.model+ " (" + this.type+") engine is being upgraded");
    }
}

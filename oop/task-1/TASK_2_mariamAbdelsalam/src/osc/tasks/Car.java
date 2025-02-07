package osc.tasks;

public class Car extends Vehicle implements Ubgradeable  {

    public Car(String model) {
        super(model);
        this.type="Car";
    }
    @Override
    public void displayInfo(){
        System.out.println("This is a car \n model: "+ this.getModel()+"\n type: "+this.getType());
    }
    @Override
    public void upgradeEngine(){
        System.out.println(this.model+ " (" + this.type+") engine is being upgraded");
    }

}

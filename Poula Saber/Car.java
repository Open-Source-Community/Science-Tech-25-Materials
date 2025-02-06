public class Car extends Vehicle implements Upgradeable{


    public Car(String model, String name) {
        super(model, name, "Car");
    }

    @Override
    public String displayInfo() {
        return "Car{" +
                "model='" + super.getModel() + '\'' +
                ", name='" + super.getName() + '\'' +
                ", type='" + super.getType() + '\'' +
                '}';
    }



    @Override
    public void upgradeEngine() {

        System.out.println(this.getName() + " " + this.getModel()+ " (" + this.getType() + ") engine is being upgraded." );
    }
}

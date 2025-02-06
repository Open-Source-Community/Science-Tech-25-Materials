public class Bike extends Vehicle implements Upgradeable{
    public Bike (String model, String name) {
        super(model, name, "Bike");
    }
    @Override
    public String displayInfo() {
        return "Bike{" +
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

public class Bike extends UpgradableVehicle{
    public Bike(String m,String n) {
        super("Bike",m,n);
    }

    @Override
    void display_info() {
        System.out.println(" this is a bike :"+ name +" "+model);
    }
}

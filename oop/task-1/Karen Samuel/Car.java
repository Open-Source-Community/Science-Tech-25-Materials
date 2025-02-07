public class Car extends UpgradableVehicle {
    public Car(String m,String n) {
        super("car",m,n);


    }

    @Override
    void display_info() {
        System.out.println(" this is a car :"+ name +" "+model);

    }
}

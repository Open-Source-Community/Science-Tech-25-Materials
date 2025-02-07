public class Main {
    public static void main(String[] args) {
        Car l =new Car("Model s","Tesla");
        Bike k =new Bike("Davidson","Harley");
        l.startvehicle();
        l.accelratevehicle();
        l.stopvehicle();
        k.startvehicle();
        k.accelratevehicle();
        k.stopvehicle();
        l.upgradeEngine();
        k.upgradeEngine();
        l.display_info();
        k.display_info();
    }

}
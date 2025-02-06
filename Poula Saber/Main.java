public class Main {
    public static void main(String[] args) {

Car t= new Car("Tesla"," Model S");
        Bike b= new Bike("Harley"," Davidson");
t.startVehicle();
t.accelerateVehicle();
t.stopVehicle();
b.startVehicle();
b.accelerateVehicle();
b.stopVehicle();
t.upgradeEngine();
b.upgradeEngine();
        System.out.println(t.displayInfo());
        System.out.println(b.displayInfo());


    }
}
//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {
    public static void main(String[] args) {
Car car=new Car("suzuki model ");
        Bike bike=new Bike("sandro ");


        car.start();
        car.accelerate();
        car.stop();
        bike.start();
        bike.accelerate();
        bike.stop();

        car.upgradeEngine();
        bike.upgradeEngine();

        car.displayinfo();
        bike.displayinfo();



    }
}
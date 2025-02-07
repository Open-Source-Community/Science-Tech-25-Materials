//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {
    public static void main(String[] args) {
        Car tesla = new Car("Tesla Model S","Car");
        Bike haley = new Bike("Harley Davidson","Bike");
        tesla.start(tesla.modelName);
        tesla.accelerating(tesla.modelName, tesla.Type);
        tesla.stop();
        haley.start(haley.modelName);
        haley.accelerating(haley.modelName, haley.Type);
        haley.stop();
        tesla.upgrade(tesla.modelName, tesla.Type);
        haley.upgrade(haley.modelName, haley.Type);
        tesla.display(tesla.modelName,tesla.Type);
        haley.display(haley.modelName, haley.Type);
    }
}
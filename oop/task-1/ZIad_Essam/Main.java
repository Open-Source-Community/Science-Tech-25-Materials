
interface drivable {
    void start();
    void stop();
    void accelerate();
}
interface upgradable{
    void upgrade();
}
abstract class vehicle implements drivable {
    String color;
    String model;
    String engine;
    String gear;
    String type;
    String condtion;
    int year;


    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public String getEngine() {
        return engine;
    }

    public void setEngine(String engine) {
        this.engine = engine;
    }

    public String getGear() {
        return gear;
    }

    public void setGear(String gear) {
        this.gear = gear;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getCondtion() {
        return condtion;
    }

    public void setCondtion(String condtion) {
        this.condtion = condtion;
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }


    @Override
    public void start(){
        System.out.println(this.toString()+ " start");
    }
    public void accelerate(){
        System.out.println(this.toString()+ " accelerate");
    }
    public void stop(){
        System.out.println(this.toString()+ " stop");
    }

}
class car extends vehicle implements upgradable {
    car(String model,int year){
        this.model = model;
        this.year = year;
    }
    car(String color, String model, String engine, String gear, String type, String condtion, int year) {
        this.color = color;
        this.model = model;
        this.engine = engine;
        this.gear = gear;
        this.type = "Car";
        this.condtion = condtion;
        this.year = year;
    }

    public void upgrade(){
        System.out.println(this.toString()+ " upgrade");
    }

    @Override
    public String toString(){
        return  this.getModel()  + " (car) " + this.getYear();
    }



}
class bike extends vehicle implements upgradable {

    bike(String color, String model, String engine, String condtion, int year) {
        this.color = color;
        this.model = model;
        this.engine = engine;
        this.type = "bike";
        this.condtion = condtion;
        this.year = year;
    }

    bike(String model,int year){
        this.model = model;
        this.year = year;
    }
    public void upgrade(){
        System.out.println(this.toString()+ " upgrade");
    }

    @Override
    public String toString(){
        return  this.getModel()  + " (Bike) " + this.getYear();
    }



}






public class Main {
    public static void main(String[] args) {
        car x = new car("Tesla Model S",2024);
        bike y = new bike("Harley",2024);

        x.start();x.accelerate();x.stop();

        y.start();y.accelerate();y.stop();

        x.upgrade();
        y.upgrade();

        System.out.println(x.toString());
        System.out.println(y.toString());
    }
}
public class Car extends Vehicle implements upgrading{
    public String type="Car";

    public Car(String liecence,String color,String modelName)
    {
        this.setLiecence(liecence);
        this.color=color;
        this.modelName=modelName;

    }
    @Override
    public void display() {
        System.out.println("this is a " +type+": "+modelName);
    }

    @Override
    public void upgrade() {
        System.out.println(type+" "+modelName+" "+"engine is being upgraded");
    }
}
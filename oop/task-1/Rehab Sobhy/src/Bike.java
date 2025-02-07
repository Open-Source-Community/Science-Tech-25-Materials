public class Bike extends Vehicle implements upgrading{

    public String type ="bike";
    public Bike(String  liecence,String color,String modelName)
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
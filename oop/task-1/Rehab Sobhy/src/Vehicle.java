public  abstract class Vehicle {
    public String modelName;
    private  String liecence;
    public String color;
    public String type;
    public String getLiecence() {
        return liecence;
    }

    public void setLiecence(String liecence) {
        this.liecence = liecence;
    }
    public abstract void display();

    public void start()
    {
        System.out.println(modelName+"is starting");
    }
    public void stop()
    {
        System.out.println(modelName+"is stopping");

    }
    public void accelerate()
    {
        System.out.println(modelName+"is accelrating");

    }
}
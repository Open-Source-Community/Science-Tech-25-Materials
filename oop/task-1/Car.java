public class Car extends Vehicle implements Upgradeable {
    public Car(String modelname) {
        super(modelname);
    }

    @Override
    public void start()
    {
        System.out.println(getModelname()+"is starting.");

    }

    public void stop()
    {
        System.out.println(getModelname()+"is stopping.");
    }
    public void  accelerate()
    {
        System.out.println(getModelname()+"(car)is accelerating");
    }

    public void upgradeEngine()
    {
        System.out.println(getModelname()+"(car)engine is being upgraded");
    }


    public void displayinfo()
    {
        System.out.println("this is a car"+getModelname());
    }


}

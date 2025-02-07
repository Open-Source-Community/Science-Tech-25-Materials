public class Bike extends Vehicle implements Upgradeable{


public Bike(String modelname){
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
        System.out.println(getModelname()+"(bike)is accelerating");
    }

    public void upgradeEngine()
    {
        System.out.println(getModelname()+"(bike)engine is being upgraded");
    }


    public void displayinfo()
    {
        System.out.println("this is a bike"+getModelname());
    }





}

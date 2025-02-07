public class Vehicle extends Information{
    public Vehicle(String modelName, String type) {
        super();
    }
    @Override
    public void upgrade(String mod, String ty) {
        System.out.println(mod+"("+ty+")"+" engine is being upgraded");
    }

    @Override
    public void start(String mod) {
        System.out.println(mod+" is starting");
    }

    @Override
    public void stop() {
        System.out.println(this.modelName+" is stopping");
    }

    @Override
    public void accelerating(String mod, String ty) {
        System.out.println(mod+"("+ty+")"+" is starting");
    }

    @Override
    public void display(String modelName, String Type) {
        System.out.println("This is a "+Type+": "+modelName);
    }


}

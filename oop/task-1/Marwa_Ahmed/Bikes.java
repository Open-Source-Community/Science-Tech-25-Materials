public class Bikes extends Vehicle{
    public Bikes(String model_Name, String type) {
        super(model_Name, type);
    }

    @Override
    public void additionalFun() {
        System.out.println(this.model_Name+" "+"("+this.type+" )"+" engine is being upgraded.");
    }
}

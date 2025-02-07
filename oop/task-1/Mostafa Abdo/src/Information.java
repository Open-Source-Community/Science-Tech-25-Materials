public abstract class Information extends Upgradeable{
    String modelName;
    String Type;

    public abstract void start(String mod);

    public abstract void accelerating(String mod, String ty);

    public abstract void display(String modelName, String Type);
}

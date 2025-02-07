public abstract class UpgradableVehicle extends Vehicle {
    public UpgradableVehicle(String t,String m,String n) {
        super(t,m,n);
        this.upgrade = false;
    }

    boolean upgrade;

    void upgradeEngine(){
        upgrade=true;

        System.out.println(name +" "+model +" ("+Type+")"+" engine is being upgraded.");
    }
}

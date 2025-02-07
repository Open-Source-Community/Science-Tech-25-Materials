import java.util.ArrayList;

public  class Vehicle implements Drivable,Upgradeable{
    String model_Name,type;
   static ArrayList<Vehicle>vehicles=new ArrayList<>();

    public Vehicle() {
    }

    public Vehicle(String model_Name, String type) {
        this.model_Name = model_Name;
        this.type = type;
        vehicles.add(this);
    }

    @Override
    public void Accelerting() {
        System.out.println(this.model_Name+" "+"("+this.type+")"+" is accelerating.");
    }

    @Override
    public void Starting() {
        System.out.println(this.model_Name+" "+" is accelerating.");
    }

    @Override
    public void Stopping() {
        System.out.println(this.model_Name+" is accelerating.");

    }
    public  void displayInfo(){
       for(int i=0;i<vehicles.size();i++) {
          vehicles.get(i).Starting();
           vehicles.get(i).Accelerting();
           vehicles.get(i).Stopping();

       }
   for(int i=0;i<vehicles.size();i++) {
         vehicles.get(i).additionalFun();

       }
        for(int i=0;i<vehicles.size();i++) {
            System.out.println("this is a "+vehicles.get(i).type+":"+vehicles.get(i).model_Name);
        }

    }
}

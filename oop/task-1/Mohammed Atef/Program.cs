namespace Veichle_management_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            Vehicle tesla = new Car("Tesla model s ","Car","");
            tesla.startingTheVehicle();
            tesla.accelaratingTheVehicle();
           
            tesla.stopingTheVehicle();
            Vehicle Kia = new Bike("Harley Davidson k ", "Bike", "");
            Kia.startingTheVehicle();
            Kia.accelaratingTheVehicle();
            Kia.stopingTheVehicle();
            ((IUpgradeable)tesla).UpgradeEngine();
            ((IUpgradeable)Kia).UpgradeEngine();
            
            tesla.displayingInformation();
            Kia.displayingInformation();

        }
    }
}

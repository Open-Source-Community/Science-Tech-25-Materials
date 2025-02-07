public class Main {
    public static void main(String[] args) {
        Vehicle toyota = new Car("suzuki");
        Vehicle niggar = new Bike("mbappe");

        System.out.println("instagram cat car\n");
        toyota.start();
        toyota.acc();
        toyota.stop();
        toyota.displayInfo();

        System.out.println("\nNigga's bike\n");
        niggar.start();
        niggar.acc();
        niggar.stop();
        niggar.displayInfo();

        // مش قادر اعمل upgrade فعدوهالي معلش عشان زنقت نفسي لأخر يوم فالتاسك
    }
}
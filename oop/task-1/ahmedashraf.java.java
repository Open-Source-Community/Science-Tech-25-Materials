package com.mycompany.ahmedashraf;
interface drive {
    void start();
    void stop();
    void accelerate();
}
interface upgrade {
    void upgradeEngine();
}
interface vinfo {
    void display();
}
abstract class Vehicle implements drive, vinfo {
    protected String model;
    public Vehicle(String s) {
        this.model = s;
    }
    public void start() {
        System.out.println(model+"starting");
    }
    public void stop() {
        System.out.println(model+"stopping");
    }
    public void accelerate() {
        System.out.println(model+"accelerating");
    }
}
class Car extends Vehicle implements upgrade {
    public Car(String model) {
        super(model);
    }
    public void upgradeEngine() {
        System.out.println(model+" Car upgraded");
    }
    public void display() {
        System.out.println("car is "+model);
    }
}
class Bike extends Vehicle implements upgrade {
    public Bike(String model) {
        super(model);
    }
    public void upgradeEngine() {
        System.out.println(model+" Bike upgraded");
    }
    public void display() {
        System.out.println("bike "+ model);
    }
}
public class ahmedashraf {
    public static void main(String[] args) {
        Car tesla = new Car("Tesla Model S ");
        Bike harley = new Bike("Harley Davidson ");
        tesla.start();tesla.accelerate();tesla.stop();
        harley.start();harley.accelerate();harley.stop();
        tesla.upgradeEngine();harley.upgradeEngine();
        tesla.display();harley.display();
    }
}

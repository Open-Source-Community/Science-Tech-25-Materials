package com.mycompany.abdelrahman_badr;

public abstract class Vehicle {
    private String name;
    private String type;
    private float speed;
    private float acceleration_rate;
    
    Vehicle(String name){
        setName(name);
    }
    
    Vehicle(String name, String type){
        this(name);
        setType(type);
    }
    
    Vehicle(String name, String type, float speed){
        this(name, type);
        setSpeed(speed);
    }
    
    Vehicle(String name, String type, float speed, float acceleration_rate){
        this(name, type, speed);
        setAcceleration_rate(acceleration_rate);
    }
    
    
    public void setName(String name){
        if(!name.equals(""))
            this.name = name;
        else
            System.out.println("NAME MUST BE AT LEAST 1 CHARACTER LONG");
    }
    public String getName(){
        return this.name;
    }
    
    public void setType(String type){
        if(!type.equals(""))
            this.type = type;
        else
            System.out.println("TYPE MUST BE AT LEAST 1 CHARACTER LONG");
    }
    public String getType(){
        return this.type;
    }
    
    public void setSpeed(float speed){
        if(speed > 0.0f)
            this.speed = speed;
        else
            System.out.println("SPEED MUST BE POSITIVE VALUE");
    }
    public float getSpeed(float speed){
        return this.speed;
    }
    
    public void setAcceleration_rate(float acceleration_rate){
        if(acceleration_rate >= 0.0f){
            this.acceleration_rate = acceleration_rate;
        }
        else
            System.out.println("ACCELERATION_RATE CAN'T BE NEGATIVE VALUE");
    }
    public float getAcceleration_rate(float acceleration_rate){
        return this.acceleration_rate;
    }
    
    
    public void start(){
        System.out.println(name + " is starting.");
    }
    
    public void accelerate(){
        System.out.println(name + '(' + type + ')' +" is accelerating.");
    }
    
    public void stop(){
        System.out.println(name + " is stopping.");
    }
    
    public void displayInfo(){
        System.out.println("This is a " + type + ": " + name);
    }
}

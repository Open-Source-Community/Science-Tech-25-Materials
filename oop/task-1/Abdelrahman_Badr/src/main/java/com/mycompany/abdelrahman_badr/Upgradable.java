package com.mycompany.abdelrahman_badr;

public abstract class Upgradable extends Vehicle{
    Upgradable(String name, String type){
        super(name, type);
    }
    
    @Override
    public void setType(String type) {
        super.setType(type);
    }
    public void upgradeEngine(){
        System.out.println(super.getName() + "(" + super.getType() + ")" +" is accelerating.");
    }
}

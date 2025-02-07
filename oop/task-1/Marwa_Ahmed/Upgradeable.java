public interface Upgradeable {
    default void additionalFun(){
        System.out.println("upgraded");
    }
}

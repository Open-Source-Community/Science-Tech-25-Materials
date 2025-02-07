public interface Upgradeable {
    default void upgrade() {
        System.out.println("Vehicle is being upgraded");
    }
}

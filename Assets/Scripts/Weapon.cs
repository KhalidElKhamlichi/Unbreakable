public interface Weapon {
    void attack();
    bool isPickable();

    void setPickable(bool pickable);

    void pickUp();

    void drop();
}

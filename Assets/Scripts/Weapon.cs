public interface Weapon {
    void attack();
    int getDamage();
    bool isPickable();

    void setPickable(bool pickable);
}

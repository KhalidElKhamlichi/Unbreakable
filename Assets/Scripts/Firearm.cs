
using UnityEngine;

public class Firearm : MonoBehaviour, Weapon {
    
    [SerializeField] private FiringStrategy firingStrategy;
    [SerializeField] private int damage;

    public void attack() {
        firingStrategy.shoot();
    }

    public int getDamage() {
       return damage;
    }
}

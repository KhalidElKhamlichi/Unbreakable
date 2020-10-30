
using System;
using UnityEngine;

public class Firearm : MonoBehaviour, Weapon {
    [SerializeField] private GameObject projectile;
    [SerializeField] private int damage;
    
    private Transform emissionPoint;
    private FiringStrategy firingStrategy;

    private void Start() {
        emissionPoint = transform.GetChild(0);
        firingStrategy = GetComponent<FiringStrategy>();
    }

    public void attack() {
        firingStrategy.shoot(projectile, emissionPoint);
        Destroy(gameObject);
    }

    public int getDamage() {
       return damage;
    }
}


using System;
using UnityEngine;

public class Firearm : MonoBehaviour, Weapon {
    [SerializeField] private GameObject projectile;
    [SerializeField] private FiringStrategy firingStrategy;
    [SerializeField] private int damage;
    
    private Transform emissionPoint;

    private void Start() {
        emissionPoint = transform.GetChild(0);
    }

    public void attack() {
        firingStrategy.shoot(projectile, emissionPoint);
    }

    public int getDamage() {
       return damage;
    }
}

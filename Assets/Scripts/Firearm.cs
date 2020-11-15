
using System;
using UnityEngine;

public class Firearm : Weapon {
    [SerializeField] private GameObject projectile;

    private Transform emissionPoint;
    private FiringStrategy firingStrategy;

    protected override void Start() {
        base.Start();
        emissionPoint = transform.GetChild(0);
        firingStrategy = GetComponent<FiringStrategy>();
    }

    public override void attack() {
        base.attack();
        firingStrategy.shoot(projectile, emissionPoint);
        Destroy(gameObject);
    }
}

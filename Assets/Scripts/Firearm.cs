
using System;
using UnityEngine;

public class Firearm : Weapon {
    [SerializeField] private GameObject projectile;

    private Transform emissionPoint;
    private FiringStrategy firingStrategy;
    private event Action onAttackEvent;

    protected override void Start() {
        base.Start();
        emissionPoint = transform.GetChild(0);
        firingStrategy = GetComponent<FiringStrategy>();
    }

    public override void attack() {
        firingStrategy.shoot(projectile, emissionPoint);
        onAttackEvent?.Invoke();
        Destroy(gameObject);
    }

    public void onAttack(Action action) => onAttackEvent += action;
}


using System;
using UnityEngine;

public class Firearm : Weapon {
    [SerializeField] private GameObject projectile;
    [SerializeField] private float firerate;
    [SerializeField] private int clipSize = 1;

    private Transform emissionPoint;
    private FiringStrategy firingStrategy;
    private float timer;

    protected override void Start() {
        base.Start();
        emissionPoint = transform.GetChild(0);
        firingStrategy = GetComponent<FiringStrategy>();
    }

    private void Update() {
        timer -= Time.deltaTime;
    }
    public override void attack() {
        if (timer > 0) return;
        base.attack();
        firingStrategy.shoot(projectile, emissionPoint);
        clipSize--;
        timer = 1 / firerate;
        if(clipSize == 0) Destroy(gameObject);
    }
}

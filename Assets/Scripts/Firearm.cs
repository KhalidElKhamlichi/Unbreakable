
using System;
using UnityEngine;

public class Firearm : Weapon {
    [SerializeField] private float firerate;
    [SerializeField] private int clipSize = 1;
    [SerializeField] private FiringStrategy firingStrategy;

    private Transform emissionPoint;
    private float timer;

    protected override void Start() {
        base.Start();
        emissionPoint = transform.GetChild(0);
    }

    private void Update() {
        timer -= Time.deltaTime;
    }
    public override void attack() {
        if (timer > 0) return;
        base.attack();
        firingStrategy.shoot(emissionPoint);
        clipSize--;
        timer = 1 / firerate;

        if (clipSize == 0) {
            usable = false;
            Destroy(gameObject);
        }
    }
}

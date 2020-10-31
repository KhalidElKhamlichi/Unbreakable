using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, Damager {
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;

    private Rigidbody2D rbody;
    private void Start() {
        rbody = GetComponent<Rigidbody2D>();
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return knockbackForce;
    }
}

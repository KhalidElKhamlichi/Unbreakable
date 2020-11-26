using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, Damager {
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return knockbackForce;
    }
}

using System;
using UnityEngine;

public class Rocket : MonoBehaviour, Damager {
    
    [SerializeField] private float radius;
    [SerializeField] private int damage;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;

    private ScreenShake screenShake;
    private BasicExplosion explosion;

    private void Start() {
        screenShake = FindObjectOfType<ScreenShake>();
        explosion = new BasicExplosion(gameObject, radius);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        explosion.start();
        screenShake.shake(shakeAmount, shakeDuration);
        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, radius);
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        throw new NotImplementedException();
    }
}

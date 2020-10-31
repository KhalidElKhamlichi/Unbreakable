using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour, Damager {
    
    [SerializeField] private int radius;
    [SerializeField] private GameObject explosionEffect;
    private void OnTriggerEnter2D(Collider2D other) {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hitCollider in hitColliders) {
            if(hitCollider.CompareTag("Enemy")) Destroy(hitCollider.gameObject);
        }

        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, radius);
    }

    public int getDamage() {
        throw new NotImplementedException();
    }

    public float getKnockbackForce() {
        throw new NotImplementedException();
    }
}

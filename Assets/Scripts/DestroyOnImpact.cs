﻿using UnityEngine;

namespace Unbreakable {
    public class DestroyOnImpact : MonoBehaviour {

        [SerializeField] private GameObject impactEffect;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if(impactEffect) Instantiate(impactEffect, other.ClosestPoint(transform.position), transform.rotation);
            Destroy(gameObject);
        }
    }
}

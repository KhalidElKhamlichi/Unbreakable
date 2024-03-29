﻿using UnityEngine;

namespace Unbreakable.Combat {
    public class Projectile : MonoBehaviour, Damager {
        [SerializeField] private int damage;
        [SerializeField] private float knockbackForce;

        public int getDamage() {
            return damage;
        }

        public float getKnockBackForce() {
            return knockbackForce;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, Weapon, Damager
{
    [SerializeField] private int damage;
    
    private bool pickable = true;
    
    public void attack() {
        GetComponent<Collider2D>().enabled = true;
        Destroy(gameObject, .5f);
    }

    public int getDamage() {
        return damage;
    }

    public void setPickable(bool pickable) {
        this.pickable = pickable;
        Invoke(nameof(resetPickable), 1f);
    }

    public bool isPickable() {
        return pickable;
    }
    
    private void resetPickable() {
        pickable = true;
    }
}

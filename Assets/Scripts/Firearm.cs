
using System;
using UnityEngine;

public class Firearm : MonoBehaviour, Weapon {
    [SerializeField] private GameObject projectile;
    
    private Transform emissionPoint;
    private FiringStrategy firingStrategy;
    private bool pickable = true;

    private void Start() {
        emissionPoint = transform.GetChild(0);
        firingStrategy = GetComponent<FiringStrategy>();
    }

    public void attack() {
        firingStrategy.shoot(projectile, emissionPoint);
        Destroy(gameObject);
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

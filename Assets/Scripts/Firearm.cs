
using System;
using UnityEngine;

public class Firearm : MonoBehaviour, Weapon {
    [SerializeField] private GameObject projectile;
    [SerializeField] private Sprite spriteWithArms;
    [SerializeField] private Sprite spriteWithoutArms;
    [SerializeField] private bool pickable = true;
    
    private Transform emissionPoint;
    private FiringStrategy firingStrategy;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        emissionPoint = transform.GetChild(0);
        firingStrategy = GetComponent<FiringStrategy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickable ? spriteWithoutArms : spriteWithArms;
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

    public void pickUp() {
        spriteRenderer.sprite = spriteWithArms;
    }

    public void drop() {
        spriteRenderer.sprite = spriteWithoutArms;
    }
    
    private void resetPickable() {
        pickable = true;
    }
}

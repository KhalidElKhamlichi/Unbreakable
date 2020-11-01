using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, Weapon, Damager
{
    [SerializeField] private int damage;
    [SerializeField] private Sprite spriteWithArms;
    [SerializeField] private Sprite spriteWithoutArms;
    
    private bool pickable = true;
    private SpriteRenderer spriteRenderer;
    
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void attack() {
        GetComponent<Collider2D>().enabled = true;
        
        Destroy(gameObject, .5f);
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return 50;
    }

    public void setPickable(bool pickable) {
        this.pickable = pickable;
        Invoke(nameof(resetPickable), 1f);
    }

    public void pickUp() {
        spriteRenderer.sprite = spriteWithArms;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void drop() {
        spriteRenderer.sprite = spriteWithoutArms;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public bool isPickable() {
        return pickable;
    }
    
    private void resetPickable() {
        pickable = true;
    }
}

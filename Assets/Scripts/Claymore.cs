using System;
using DG.Tweening;
using UnityEngine;

public class Claymore : MonoBehaviour, Weapon
{
    
    [SerializeField] private int radius;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Sprite spriteWithArms;
    [SerializeField] private Sprite spriteWithoutArms;
    [SerializeField] private GameObject fieldOfEffect;

    private SpriteRenderer spriteRenderer;
    private bool pickable = true;
    private bool isActive;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!isActive || !other.CompareTag("Enemy")) return;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius+radius/4);
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
    public void attack() {
        isActive = true;
        pickable = false;
        GetComponent<CircleCollider2D>().radius = radius;
        gameObject.transform.parent = null;
        spriteRenderer.sprite = spriteWithoutArms;
        gameObject.transform.DOJump(transform.position + transform.right, 1, 1, .3f, false)
            .onComplete += () => Instantiate(fieldOfEffect, transform.position, Quaternion.identity).transform.parent = transform; 
        
    }

    public bool isPickable() {
        return pickable;
    }

    public void setPickable(bool pickable) {
        this.pickable = pickable;
        if(!pickable) isActive = true;
    }

    public void pickUp() {
        spriteRenderer.sprite = spriteWithArms;
    }

    public void drop() {
        spriteRenderer.sprite = spriteWithoutArms;
        Instantiate(fieldOfEffect, transform.position, Quaternion.identity).transform.parent = transform;
    }
}

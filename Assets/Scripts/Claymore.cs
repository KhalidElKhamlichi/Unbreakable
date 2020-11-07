using DG.Tweening;
using UnityEngine;

public class Claymore : Weapon
{
    [SerializeField] private int radius;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject fieldOfEffect;
    
    private bool isActive;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!isActive || !other.CompareTag("Enemy")) return;
        float damaginRadius = radius+radius*.8f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, damaginRadius);
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
    public override void attack() {
        isActive = true;
        pickable = false;
        GetComponent<CircleCollider2D>().radius = radius;
        gameObject.transform.parent = null;
        spriteRenderer.sprite = spriteWithoutArms;
        gameObject.transform.DOJump(transform.position + transform.right, 1, 1, .3f, false)
            .onComplete += () => Instantiate(fieldOfEffect, transform.position, Quaternion.identity).transform.parent = transform; 
        
    }

    public override void drop() {
        base.drop();
        Instantiate(fieldOfEffect, transform.position, Quaternion.identity).transform.parent = transform;
    }
}

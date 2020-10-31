using DG.Tweening;
using UnityEngine;

public class Claymore : MonoBehaviour, Weapon
{
    
    [SerializeField] private int radius;
    [SerializeField] private GameObject explosionEffect;

    private bool pickable = true;
    private bool isActive;
    private void OnTriggerEnter2D(Collider2D other) {
        if (!isActive || !other.CompareTag("Enemy")) return;
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
    public void attack() {
        isActive = true;
        pickable = false;
        GetComponent<CircleCollider2D>().radius = .6f;
        gameObject.transform.parent = null;
        gameObject.transform.DOJump(transform.position + transform.right, 1, 1, .3f, false); 
    }

    public bool isPickable() {
        return pickable;
    }

    public void setPickable(bool pickable) {
        this.pickable = pickable;
        if(!pickable) isActive = true;
    }
}

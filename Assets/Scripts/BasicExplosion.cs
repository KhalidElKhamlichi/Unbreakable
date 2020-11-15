
using Unbreakable;
using UnityEngine;

public class BasicExplosion {
    private readonly GameObject damager;
    private readonly float damagingRadius;
    private readonly string tag;

    public BasicExplosion(GameObject damager, float damagingRadius, string tagOverride = "") {
        this.damager = damager;
        this.damagingRadius = damagingRadius;
        tag = string.IsNullOrEmpty(tagOverride) ? damager.tag : tagOverride;
    }

    public void start() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(damager.transform.position, damagingRadius);
        foreach (Collider2D hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Enemy")) {
                hitCollider.GetComponent<CollisionManager>()?
                    .invokeHitEvent(new HitInfo(damager.GetComponent<Damager>(), tag, damager.transform.position - hitCollider.transform.position));
            }
        }
    }
}

using DG.Tweening;
using UnityEngine;

public class Claymore : Weapon, Damager
{
    [SerializeField] private float triggerRadius;
    [SerializeField] private int damage;
    [SerializeField] private float damagingRadiusMultiplier;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject fieldOfEffect;
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;

    private ScreenShake screenShake;
    private BasicExplosion explosion;
    private bool isActive;
    private void Start() {
        base.Start();
        explosion = new BasicExplosion(gameObject, triggerRadius * damagingRadiusMultiplier, "Damager");
        screenShake = FindObjectOfType<ScreenShake>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!isActive || !other.CompareTag("Enemy")) return;
        explosion.start();
        screenShake.shake(shakeAmount, shakeDuration);
        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, triggerRadius);
    }
    public override void attack() {
        base.attack();
        isActive = true;
        pickable = false;
        usable = false;
        GetComponent<CircleCollider2D>().radius = triggerRadius;
        gameObject.transform.parent = null;
        spriteRenderer.sprite = spriteWithoutArms;
        gameObject.transform.DOJump(transform.position + transform.right, 1, 1, .3f, false)
            .onComplete += () => Instantiate(fieldOfEffect, transform.position, Quaternion.identity).transform.parent = transform;
    }

    public override void drop() {
        base.drop();
        Instantiate(fieldOfEffect, transform.position, Quaternion.identity).transform.parent = transform;
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        throw new System.NotImplementedException();
    }
}

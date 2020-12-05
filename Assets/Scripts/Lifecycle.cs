using System;
using Unbreakable;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
public class Lifecycle : MonoBehaviour {
    [SerializeField] int maxHP;
    
    private event Action<GameObject> deathEvent;
    private event Action<int> takeDamageEvent;
    private int currentHP;

    protected virtual void Start() {
        currentHP = maxHP;
        GetComponent<CollisionManager>().onHit(takeDamage);
    }

    public void onDeath(Action<GameObject> onDeath) {
        deathEvent += onDeath;
    }
    
    public void onTakeDamage(Action<int> onTakeDamage) {
        takeDamageEvent += onTakeDamage;
    }

    protected virtual void takeDamage(HitInfo hit) {
        Damager damager = hit.getDamager();
        if (damager == null) return;
        currentHP -= damager.getDamage();
        takeDamageEvent?.Invoke(currentHP);
        checkLife();
    }

    private void checkLife() {
        if (currentHP <= 0) {
            deathEvent?.Invoke(gameObject);
            deathEvent = null;
            onDeath();
        }
    }

    protected virtual void onDeath() {
        Destroy(transform.parent != null ? transform.parent.gameObject : gameObject, .1f);
    }

    public float getMaxHp() {
        return maxHP;
    }

    private void OnDestroy() {
        deathEvent?.Invoke(gameObject);
    }
}

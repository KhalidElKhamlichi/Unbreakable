using System;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
public class Lifecycle : MonoBehaviour {
    [SerializeField] int maxHP;
    
    private event Action<GameObject> deathEvent;
    private int currentHP;

    protected virtual void Start() {
        currentHP = maxHP;
        GetComponent<CollisionManager>().onHit(takeDamage);
    }

    public void onDeath(Action<GameObject> onDeath) {
        deathEvent += onDeath;
    }

    protected virtual void takeDamage(Collider2D other) {
        Damager damager = other.gameObject.GetComponent<Damager>();
        if (damager == null) return;
        
        currentHP -= damager.getDamage();
        checkLife();
    }

    private void checkLife() {
        if (currentHP <= 0) {
            deathEvent?.Invoke(gameObject);
            onDeath();
        }
    }

    protected virtual void onDeath() {
        Destroy(transform.parent != null ? transform.parent.gameObject : gameObject, .1f);
    }

    public float getCurrentHp() {
        return currentHP;
    }

    public float getMaxHp() {
        return maxHP;
    }

    private void OnDestroy() {
        deathEvent?.Invoke(gameObject);
    }
}

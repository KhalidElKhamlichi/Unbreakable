using System;
using UnityEngine;

public abstract class Weapon :  MonoBehaviour {
    
    [SerializeField] protected bool pickable = true;
    [SerializeField] protected Sprite spriteWithArms;
    [SerializeField] protected Sprite spriteWithoutArms;
    [SerializeField] protected GameObject pickupFX;
    
    protected SpriteRenderer spriteRenderer;
    protected event Action onAttackEvent;
    protected event Action onDestroyedEvent;

    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickable ? spriteWithoutArms : spriteWithArms;
    }

    public virtual void attack() {
        onAttackEvent?.Invoke();
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
        Instantiate(pickupFX);
    }

    public virtual void drop() {
        spriteRenderer.sprite = spriteWithoutArms;
    }
    
    private void resetPickable() {
        pickable = true;
    }

    private void OnDestroy() {
        onDestroyedEvent?.Invoke();
    }

    public void onAttack(Action action) => onAttackEvent += action;
    
    public void onDestroyed(Action action) => onDestroyedEvent += action;
    
    
}

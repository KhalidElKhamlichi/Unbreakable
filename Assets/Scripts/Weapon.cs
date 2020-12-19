using System;
using UnityEngine;

public abstract class Weapon :  MonoBehaviour {
    
    [SerializeField] protected bool pickable = true;
    [SerializeField] protected Sprite spriteWithArms;
    [SerializeField] protected Sprite spriteWithoutArms;
    [SerializeField] protected GameObject pickupFX;
    [SerializeField] protected int initialUses = 1;
    
    protected SpriteRenderer spriteRenderer;
    protected bool usable = true;
    protected int remainingUses;
    protected event Action attackEvent;
    protected event Action destroyedEvent;

    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickable ? spriteWithoutArms : spriteWithArms;
        remainingUses = initialUses;
    }

    public virtual void attack() {
        remainingUses--;
        attackEvent?.Invoke();
    }

    public bool isUsable() {
        return usable;
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
        if(remainingUses <= 0) destroyedEvent?.Invoke();
    }

    public void onAttack(Action action) => attackEvent += action;
    
    public void onDestroyed(Action action) => destroyedEvent += action;
    
    public int getInitialUses() {
        return initialUses;
    }
    
    public int getRemainingUses() {
        return remainingUses;
    }
}

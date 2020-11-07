using UnityEngine;

public abstract class Weapon :  MonoBehaviour {
    
    [SerializeField] protected bool pickable = true;
    [SerializeField] protected Sprite spriteWithArms;
    [SerializeField] protected Sprite spriteWithoutArms;
    
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickable ? spriteWithoutArms : spriteWithArms;
    }

    public abstract void attack();
    public void setPickable(bool pickable) {
        this.pickable = pickable;
        Invoke(nameof(resetPickable), 1f);
    }

    public bool isPickable() {
        return pickable;
    }

    public void pickUp() {
        spriteRenderer.sprite = spriteWithArms;
    }

    public virtual void drop() {
        spriteRenderer.sprite = spriteWithoutArms;
    }
    
    private void resetPickable() {
        pickable = true;
    }
}

using Unbreakable;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
[RequireComponent(typeof(Lifecycle))]
public class CharacterFX : MonoBehaviour {

    [SerializeField] private GameObject hitFX;
    [SerializeField] private GameObject deathFX;

    private void Start() {
        GetComponent<Lifecycle>().onDeath(spawnDeathFX);
        GetComponent<CollisionManager>().onHit(spawnHitFX);
    }

    private void spawnDeathFX(GameObject obj) {
        if(deathFX) Invoke(nameof(instantiateDeathFX), .1f);
    }

    private GameObject instantiateDeathFX() {
        return Instantiate(deathFX, transform.position, Quaternion.identity);
    }

    private void spawnHitFX(HitInfo hit) {
        if(hitFX) Instantiate(hitFX, transform.position, Quaternion.identity);
    }
}

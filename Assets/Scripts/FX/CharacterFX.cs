using System.Collections.Generic;
using Unbreakable;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
[RequireComponent(typeof(Lifecycle))]
public class CharacterFX : MonoBehaviour {

    [SerializeField] private List<GameObject> hitFX;
    [SerializeField] private List<GameObject> deathFX;

    private void Start() {
        GetComponent<Lifecycle>().onDeath(spawnDeathFX);
        GetComponent<CollisionManager>().onHit(spawnHitFX);
    }

    private void spawnDeathFX(GameObject obj) {
        if(deathFX.Count > 0) Invoke(nameof(instantiateDeathFX), .1f);
    }

    private GameObject instantiateDeathFX() {
        return Instantiate(pickRandom(deathFX), transform.position, Quaternion.identity);
    }

    private GameObject pickRandom(List<GameObject> gameObjects) {
        int randomIndex = Random.Range(0, gameObjects.Count);
        return gameObjects[randomIndex];
    }

    private void spawnHitFX(HitInfo hit) {
        if(hitFX.Count > 0) Instantiate(pickRandom(hitFX), transform.position, Quaternion.identity);
    }
}

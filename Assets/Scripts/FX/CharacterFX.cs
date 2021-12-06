using System.Collections.Generic;
using UnityEngine;

namespace Unbreakable.FX {
    [RequireComponent(typeof(CollisionManager))]
    [RequireComponent(typeof(Lifecycle))]
    public class CharacterFX : MonoBehaviour {

        [SerializeField] private List<GameObject> hitFX;
        [SerializeField] private List<GameObject> deathVFX;
        [SerializeField] private GameObject deathSFX;
        [SerializeField] private GameObject postDeathObject;

        private void Awake() {
            GetComponent<Lifecycle>().onDeath(spawnDeathFX);
            GetComponent<CollisionManager>().onHit(spawnHitFX);
            GetComponent<Lifecycle>().onDeath(spawnDeathObject);
        }

        private void spawnDeathFX(GameObject obj) {
            Fade fade = GetComponent<Fade>();
            if(fade) fade.enabled = true;
            if(deathVFX.Count > 0) Invoke(nameof(instantiateDeathFX), .1f);
        }
        [ContextMenu("instantiateDeathFX")]
        private void instantiateDeathFX() {
            Instantiate(pickRandom(deathVFX), transform.position, Quaternion.identity);
            Instantiate(deathSFX, transform.position, Quaternion.identity);
        }

        private void spawnDeathObject(GameObject obj) {
            Invoke(nameof(instantiateDeathObject), .5f);
        }
    
        private void instantiateDeathObject() {
            GameObject deathGameObjectClone = Instantiate(postDeathObject, transform.position, transform.rotation);
            Rigidbody2D deathGameObjectRigidbody = deathGameObjectClone.GetComponent<Rigidbody2D>();
            deathGameObjectRigidbody.AddForce(new Vector2(Random.Range(-.3f, .3f), 1.5f) * 3, ForceMode2D.Impulse);
        }
    
        private void spawnHitFX(HitInfo hit) {
            if(hitFX.Count > 0) Instantiate(pickRandom(hitFX), transform.position, Quaternion.identity);
        }
        
        private GameObject pickRandom(List<GameObject> gameObjects) {
            int randomIndex = Random.Range(0, gameObjects.Count);
            return gameObjects[randomIndex];
        }

    }
}

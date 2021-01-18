using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unbreakable.FX {
    public class WeaponBreakFX : MonoBehaviour {
        [SerializeField] private List<GameObject> scrapParts;
        [SerializeField] private int nbrOfPartsToSpawn;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
    
        [ContextMenu("Spawn")]
        private void Start() {
            instantiateScrapParts();
            Invoke(nameof(playAudio), .1f);
        }

        private void playAudio() {
            GetComponent<AudioSource>().Play();
        }

        private void instantiateScrapParts() {
            for (int i = 0; i < nbrOfPartsToSpawn; i++) {
                GameObject randomPart = scrapParts[Random.Range(0, scrapParts.Count)];
                GameObject randomPartClone = Instantiate(randomPart, transform.position, Quaternion.identity);
                applyForce(randomPartClone);
            }
        }

        private void applyForce(GameObject randomPart) {
            float force = Random.Range(minForce, maxForce);
            Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f)).normalized * force;
            randomPart.GetComponent<Rigidbody>().AddForce(randomDir * Time.deltaTime, ForceMode.Impulse);
        }
    }
}

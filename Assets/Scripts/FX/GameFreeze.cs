using System.Collections;
using UnityEngine;

namespace Unbreakable.FX {
    [RequireComponent(typeof(CollisionManager))]
    public class GameFreeze : MonoBehaviour {
        
        [SerializeField] private float freezeTime;
        
        private bool isFrozen;
        private IEnumerator freezeCoroutine;

        private void Awake() {
            GetComponent<CollisionManager>().onHit(freezeGame);
            freezeCoroutine = resumeAfter();
        }

        private void freezeGame(HitInfo hit) {
            if (isFrozen) return;
            StartCoroutine(freezeCoroutine);
        }

        private IEnumerator resumeAfter() {
            isFrozen = true;
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(freezeTime);
            Time.timeScale = 1;
            isFrozen = false;
        }
    }
}

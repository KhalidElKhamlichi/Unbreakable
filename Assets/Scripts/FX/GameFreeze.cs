using System.Collections;
using System.Collections.Generic;
using Unbreakable;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
public class GameFreeze : MonoBehaviour {
    [SerializeField] private float freezeTime;
    private static bool isFrozen;
    private static IEnumerator freezeCoroutine;

    void Start() {
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

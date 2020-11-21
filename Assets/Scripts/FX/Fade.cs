using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Fade : MonoBehaviour {
    [SerializeField] private float timeToFade;
    [SerializeField] private float initialDelay;
    private Color initialColor;
    private Color targetColor;
    private float timer;
    private SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
    }

    void Update() {
        initialDelay -= Time.deltaTime;
        if (initialDelay > 0) return;
        timer += Time.deltaTime;
        spriteRenderer.color = Color.Lerp(initialColor, targetColor, timer/timeToFade);
    }
}

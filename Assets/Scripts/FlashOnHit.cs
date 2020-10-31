using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
public class FlashOnHit : MonoBehaviour
{
    [SerializeField] float flashTime;
    [SerializeField] float numberOfFlashes;
	
    private SpriteRenderer spriteRenderer;
    private float currentNumberOfFlashes;
    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentNumberOfFlashes = numberOfFlashes;
        GetComponent<CollisionManager>().onHit(startFlashing);
    }

    private void startFlashing(Collider2D other) {
        if (isFlashing()) return;

        flash();
    }

    private void flash() {
        spriteRenderer.material.SetFloat(FlashAmount, .7f);
        Invoke(nameof(resetColor), flashTime);
        currentNumberOfFlashes--;
    }

    private bool isFlashing() {
        return !(Math.Abs(spriteRenderer.material.GetFloat(FlashAmount)) < float.Epsilon);
    }

    private void resetColor() {
        spriteRenderer.material.SetFloat(FlashAmount, 0f);
        if (currentNumberOfFlashes > 0)
            Invoke(nameof(flash), flashTime);
        else
            currentNumberOfFlashes = numberOfFlashes;

    }
}

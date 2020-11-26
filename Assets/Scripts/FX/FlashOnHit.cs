using System;
using System.Collections;
using System.Collections.Generic;
using Unbreakable;
using UnityEngine;

[RequireComponent(typeof(CollisionManager))]
public class FlashOnHit : MonoBehaviour
{
    [SerializeField] float flashTime;
    [SerializeField] float numberOfFlashes;
    [SerializeField] Color flashColor;
	
    private static readonly int FlashColor = Shader.PropertyToID("_FlashColor");
    private Material material;
    private float currentNumberOfFlashes;
    private Color initialColor;
    private bool isFlashing => material.GetColor(FlashColor).Equals(flashColor);

    private void OnValidate() {
        if(!GetComponent<SpriteRenderer>().sharedMaterial.HasProperty(FlashColor)) throw new Exception("Gameobject material doesn't have FlashAmount property");
    }

    void Start () {
        material = GetComponent<SpriteRenderer>().material;
        initialColor = material.GetColor(FlashColor);
        currentNumberOfFlashes = numberOfFlashes;
        GetComponent<CollisionManager>().onHit(startFlashing);
    }

    private void startFlashing(HitInfo hit) {
        if (isFlashing) return;
        flash();
    }

    private void flash() {
        material.SetColor(FlashColor, flashColor);
        Invoke(nameof(resetColor), flashTime);
        currentNumberOfFlashes--;
    }

    

    private void resetColor() {
        material.SetColor(FlashColor, initialColor);
        if (currentNumberOfFlashes > 0)
            Invoke(nameof(flash), flashTime);
        else
            currentNumberOfFlashes = numberOfFlashes;

    }
}

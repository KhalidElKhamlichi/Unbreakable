using System;
using Unbreakable.Combat;
using Unbreakable.Common;
using UnityEngine;

namespace Unbreakable.FX {
    [RequireComponent(typeof(CollisionManager))]
    public class FlashOnHit : MonoBehaviour
    {
        private static readonly int FLASH_COLOR_ID = Shader.PropertyToID("_FlashColor");
        
        [SerializeField] private float flashTime;
        [SerializeField] private float numberOfFlashes;
        [SerializeField] private Color flashColor;
	
        private Material material;
        private float currentNumberOfFlashes;
        private Color initialColor;
        private bool isFlashing => material.GetColor(FLASH_COLOR_ID).Equals(flashColor);
        
        private void Awake () {
            material = GetComponent<SpriteRenderer>().material;
            initialColor = material.GetColor(FLASH_COLOR_ID);
            currentNumberOfFlashes = numberOfFlashes;
            GetComponent<CollisionManager>().onHit(startFlashing);
        }

        private void OnValidate() {
            if(!GetComponent<SpriteRenderer>().sharedMaterial.HasProperty(FLASH_COLOR_ID))
                throw new Exception("Renderer material doesn't have FlashAmount property");
        }
        
        private void startFlashing(HitInfo hit) {
            if (isFlashing) return;
            flash();
        }

        private void flash() {
            material.SetColor(FLASH_COLOR_ID, flashColor);
            Invoke(nameof(resetColor), flashTime);
            currentNumberOfFlashes--;
        }

        private void resetColor() {
            material.SetColor(FLASH_COLOR_ID, initialColor);
            if (currentNumberOfFlashes > 0)
                Invoke(nameof(flash), flashTime);
            else
                currentNumberOfFlashes = numberOfFlashes;

        }
    }
}

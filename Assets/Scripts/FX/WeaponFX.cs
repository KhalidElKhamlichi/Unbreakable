using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFX : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;
    [SerializeField] private GameObject attackFX;
    [SerializeField] private GameObject breakFX;

    private ScreenShake screenShake;
    void Start() {
        screenShake = FindObjectOfType<ScreenShake>();
        Weapon weapon = GetComponent<Weapon>();
        weapon?.onAttack(shake);
        weapon?.onDestroyed(playAttackFX);
        weapon?.onDestroyed(playBreakFX);
    }

    private void playAttackFX() {
        Instantiate(attackFX);
    }
    
    private void playBreakFX() {
        Instantiate(breakFX, transform.position, transform.rotation); 
    }

    private void shake() {
        screenShake.shake(shakeAmount, shakeDuration);
    }
}

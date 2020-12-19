using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponFX : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;
    [SerializeField] private GameObject attackFX;
    [SerializeField] private GameObject breakFX;
    [SerializeField] GameObject projectileShell;
    [SerializeField] float shellSpread = 0.6f;
    [SerializeField] float shellSpeed = 250.0f;
    [SerializeField] int nbrOfshells;

    private ScreenShake screenShake;
    private Transform emissionPoint;
    void Start() {
        screenShake = FindObjectOfType<ScreenShake>();
        Weapon weapon = GetComponent<Weapon>();
        emissionPoint = transform.GetChild(0);
        weapon?.onAttack(shake);
        weapon?.onAttack(playAttackFX);
        weapon?.onDestroyed(playBreakFX);
    }

    private void playAttackFX() {
        Instantiate(attackFX, emissionPoint.position, emissionPoint.rotation);
        for (int i = 0; i < nbrOfshells; i++) {
            instantiateShell();
        }

    }

    private void instantiateShell() {
        GameObject shellClone = Instantiate(projectileShell, transform.position, transform.rotation);
        Rigidbody2D shellRigidbody = shellClone.GetComponent<Rigidbody2D>();
        shellRigidbody.AddForce(new Vector2(Random.Range(-shellSpread, shellSpread), shellSpread) * shellSpeed, ForceMode2D.Impulse);
    }

    private void playBreakFX() {
        Instantiate(breakFX, transform.position, transform.rotation); 
    }

    private void shake() {
        screenShake.shake(shakeAmount, shakeDuration);
    }
}

using Unbreakable.Combat;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unbreakable.FX {
    public class WeaponFX : MonoBehaviour
    {
        [SerializeField] private float shakeAmount;
        [SerializeField] private float shakeDuration;
        [SerializeField] private GameObject attackFX;
        [SerializeField] private GameObject breakFX;
        [SerializeField] private GameObject projectileShell;
        [SerializeField] private float shellSpread = 0.6f;
        [SerializeField] private float shellSpeed = 250.0f;
        [SerializeField] private int nbrOfShells;
        [SerializeField] private Transform emissionPoint;

        private ScreenShake screenShake;
        private void Awake() {
            screenShake = FindObjectOfType<ScreenShake>();
            Weapon weapon = GetComponent<Weapon>();
            weapon?.onAttack(shake);
            weapon?.onAttack(playAttackFX);
            weapon?.onDestroyed(playBreakFX);
        }

        private void playAttackFX() {
            Instantiate(attackFX, emissionPoint.position, emissionPoint.rotation);
            for (int i = 0; i < nbrOfShells; i++) {
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
}

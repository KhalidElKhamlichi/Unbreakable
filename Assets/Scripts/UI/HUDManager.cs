using System;
using System.Collections;
using TMPro;
using Unbreakable.Player;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Unbreakable.UI {
    public class HUDManager : MonoBehaviour {
        private const string NEXT_WAVE_TEXT = "NEXT WAVE IN: ";
        private const string TIMER_FORMAT = @"mm\:ss";

        [SerializeField] private FloatVariable currencyAmount;
        [Header("HP")]
        [SerializeField] private Image hpBar;
        [SerializeField] private Image hpBarDecay;
        [SerializeField] private float hpDecaySpeed;
        [Header("UI elements")]
        [SerializeField] private GameObject timerUI;
        [SerializeField] private TextMeshProUGUI currencyTextMesh;
        [SerializeField] private TextMeshProUGUI ammoTextMesh;

        private Lifecycle life;
        private TextMeshProUGUI timerTextMesh;
        private PlayerManager playerManager;

        private void Awake() {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            life = player.GetComponent<Lifecycle>();
            life.onTakeDamage(updateHpBar);
            playerManager = player.GetComponent<PlayerManager>();
            playerManager.onAttack(updateAmmo);
            playerManager.onWeaponPickedUp(updateAmmo);
            // resubscribe to new weapon's attack event on pick up
            playerManager.onWeaponPickedUp(() => playerManager.onAttack(updateAmmo));
        }

        private void Start () {
            currencyAmount.onChangedEvent += updateCurrency;
            timerTextMesh = timerUI.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void updateHpBar(int currentHp) {
            hpBar.fillAmount = currentHp / life.getMaxHp();
            if (hpBarDecay.fillAmount <= hpBar.fillAmount)
                hpBarDecay.fillAmount = hpBar.fillAmount;
            else
                StartCoroutine(decayHpBar());
        }

        private IEnumerator decayHpBar() {
            yield return new WaitForSeconds(.3f);
            while (hpBarDecay.fillAmount > hpBar.fillAmount) {
                hpBarDecay.fillAmount -= hpDecaySpeed * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    
        private void updateCurrency() {
            currencyTextMesh.text = currencyAmount.value.ToString();
        }

        public void	updateWaveTimer(float timer) {
            StartCoroutine(displayTimer(timer));
        }

        private IEnumerator displayTimer(float timer) {
            timerUI.SetActive(true);
            while (timer >= 0) {
                timerTextMesh.text = NEXT_WAVE_TEXT + TimeSpan.FromSeconds(timer).ToString(TIMER_FORMAT);
                timer--;
                yield return new WaitForSeconds(1);
            }
            timerUI.SetActive(false);
        }

        private void updateAmmo() {
            int? initialWeaponUses = playerManager.getInitialWeaponUses();
            int? remainingWeaponUses = playerManager.getRemainingWeaponUses();
            ammoTextMesh.text = remainingWeaponUses.HasValue && initialWeaponUses.HasValue ? remainingWeaponUses.Value+"/"+initialWeaponUses.Value : "0/0";
        }
    }
}

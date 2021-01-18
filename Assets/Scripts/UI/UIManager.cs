using System;
using System.Collections;
using TMPro;
using Unbreakable.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

namespace Unbreakable.UI {
    public class UIManager : MonoBehaviour {
	
        private static readonly int FADE_IN = Animator.StringToHash("FadeIn");
        private static readonly int FADE_OUT = Animator.StringToHash("FadeOut");
        private static readonly int DISPLAY_CONTROLS = Animator.StringToHash("DisplayControls");
        private static readonly int DISPLAY_MENU = Animator.StringToHash("DisplayMenu");
        [SerializeField] private Image hpBar;
        [SerializeField] private Image hpBarDecay;
        [SerializeField] private float hpDecaySpeed;
        [SerializeField] private GameObject currencyUI;
        [SerializeField] private GameObject timerUI;
        [SerializeField] private GameObject ammoUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private TextMeshProUGUI pauseUIWaveIndex;

        private Lifecycle life;
        private Animator pauseUIAnimator;
        private PlayerManager playerManager;
        private TextMeshProUGUI currencyText;
        private TextMeshProUGUI timerText;
        private TextMeshProUGUI ammoText;

        void Start () {
            currencyText = currencyUI.GetComponentInChildren<TextMeshProUGUI>();
            timerText = timerUI.GetComponentInChildren<TextMeshProUGUI>();
            ammoText = ammoUI.GetComponentInChildren<TextMeshProUGUI>();
            pauseUIAnimator = pauseUI.GetComponent<Animator>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameManager.currencyAmountChangedEvent += updateCurrency;
            life = player.GetComponent<Lifecycle>();
            playerManager = player.GetComponent<PlayerManager>();
            playerManager.onAttack(updateAmmo);
            playerManager.onWeaponPickedUp(updateAmmo);
            playerManager.onWeaponPickedUp(() => playerManager.onAttack(updateAmmo));
            updateAmmo();
            life.onTakeDamage(updateHpBar);
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
    
        private void updateCurrency(int newAmount) {
            currencyText.text = newAmount.ToString();
        }

        public void	updateWaveTimer(float timer) {
            StartCoroutine(displayTimer(timer));
        }

        private IEnumerator displayTimer(float timer) {
            timerUI.SetActive(true);
            while (timer >= 0) {
                timerText.text = "NEXT WAVE IN: " + TimeSpan.FromSeconds(timer).ToString(@"mm\:ss");
                timer--;
                yield return new WaitForSeconds(1);
            }
            timerUI.SetActive(false);
        }

        private void updateAmmo() {
            int? initialWeaponUses = playerManager.getInitialWeaponUses();
            int? remainingWeaponUses = playerManager.getRemainingWeaponUses();
            ammoText.text = remainingWeaponUses.HasValue && initialWeaponUses.HasValue ? remainingWeaponUses.Value+"/"+initialWeaponUses.Value : "0/0";
        }
    
        // Used by onClick button event
        public void exit() {
            resume();
            SceneManager.LoadScene("Scenes/SplashScreen");
        }
    
        // Used by onClick button event
        public void resume() {
            GameManager.isGamePaused = false;
            pauseUIAnimator.SetTrigger(FADE_OUT);
            Time.timeScale = 1;
        }
    
        public void displayPauseUI() {
            pauseUIAnimator.SetTrigger(FADE_IN);
            pauseUIWaveIndex.text = "Current wave: "+WaveManager.waveIndex + "\n" + "Last cleared wave: " + PlayerPrefs.GetInt("LastClearedWaveIndex");
        }

        public void displayControls() {
            pauseUIAnimator.SetTrigger(DISPLAY_CONTROLS);
        }
    
        public void displayMenu() {
            pauseUIAnimator.SetTrigger(DISPLAY_MENU);
        }
    }
}

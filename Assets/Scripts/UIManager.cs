using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpBarDecay;
    [SerializeField] private float hpDecaySpeed;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private Lifecycle life;
    private PlayerManager playerManager;

    void Start () {
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

    public void	 updateWaveTimer(float timer) {
        displayTimer(timer);
    }

    private void displayTimer(float timer) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        
        timerText.text = timeSpan.ToString(@"mm\:ss");
    }

    private void updateAmmo() {
        int? initialWeaponUses = playerManager.getInitialWeaponUses();
        int? remainingWeaponUses = playerManager.getRemainingWeaponUses();
        ammoText.text = remainingWeaponUses.HasValue && initialWeaponUses.HasValue ? remainingWeaponUses.Value+"/"+initialWeaponUses.Value : "0/0";
    }
}

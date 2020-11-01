using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpBarDecay;
    [SerializeField] private float hpDecaySpeed;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI timerText;

    private Lifecycle life;
    private GameManager gameManager;

    void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        life = player.GetComponent<Lifecycle>();
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        //TODO remove from update to event
        updateHpBar(life.getCurrentHp(), life.getMaxHp());
        updateCurrency();
    }

    private void updateHpBar(float newHP, float maxHP)
    {
        hpBar.fillAmount = newHP / maxHP;
        hpBarDecay.fillAmount = (hpBarDecay.fillAmount <= hpBar.fillAmount) ? hpBar.fillAmount : hpBarDecay.fillAmount - hpDecaySpeed;
    }
    
    private void updateCurrency() {
        currencyText.text = GameManager.getCurrencyAmount().ToString();
    }

    public void	 updateWaveTimer(float timer) {
        displayTimer(timer);
//        else timerText.text = string.Empty;

    }

    private void displayTimer(float timer) {
        int seconds = Mathf.RoundToInt(timer % 60);

        string secondsText = seconds.ToString();
        if (seconds < 10)
            secondsText = "0" + secondsText;

        int mins;
        if (Mathf.RoundToInt(timer / 60) <= timer / 60)
            mins = Mathf.RoundToInt(timer / 60);
        else
            mins = (Mathf.RoundToInt(timer / 60) - 1);
        string minsText = mins.ToString();
        if (mins < 10)
            minsText = "0" + minsText;

        timerText.text = minsText + ":" + secondsText;
    }
}

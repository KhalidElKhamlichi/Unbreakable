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

    private Lifecycle life;

    void Start ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        life = player.GetComponent<Lifecycle>();
    }

    private void Update()
    {
        //TODO remove from update to event
        updateHpBar(life.getCurrentHp(), life.getMaxHp());
    }

    private void updateHpBar(float newHP, float maxHP)
    {
        hpBar.fillAmount = newHP / maxHP;
        hpBarDecay.fillAmount = (hpBarDecay.fillAmount <= hpBar.fillAmount) ? hpBar.fillAmount : hpBarDecay.fillAmount - hpDecaySpeed;
    }
    
    public void	 updateCurrency(int currentCurrency) {
        currencyText.text = currentCurrency.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private UIManager uiManager;
    
    public static int currentCurrency;

    public void subscribeToEnemyDeath(GameObject enemy) {
        enemy.GetComponent<Lifecycle>().onDeath(addCurrency);
    }

    private void addCurrency(GameObject obj) {
        currentCurrency += obj.GetComponent<CurrencyDrop>().getCurrencyAmount();
//        uiManager.updateCurrency(currentCurrency);
    }

    public static void reduceCurrency(int value) {
        currentCurrency -= value;
    }
    public static int getCurrencyAmount() {
        return currentCurrency;
    }
}

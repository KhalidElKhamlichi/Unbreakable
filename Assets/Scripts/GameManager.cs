using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [SerializeField] private UIManager uiManager;
    private static int currentCurrency;
    public static event Action<int> currencyAmountChangedEvent;
    public static bool isGamePaused;

    private void Awake() {
        currentCurrency = 0;
        WaveManager.waveEndedEvent += saveScore;
    }

    private void saveScore() {
        PlayerPrefs.SetInt("LastClearedWaveIndex", Mathf.Max(PlayerPrefs.GetInt("LastClearedWaveIndex"), WaveManager.waveIndex));
    }

    private void Update() {
        if (Input.GetButtonDown("Pause")) {
            if(isGamePaused) return;
            isGamePaused = true;
            uiManager.displayPauseUI();
            Time.timeScale = 0;
        }
        
    }

    public void subscribeToEnemyDeath(GameObject enemy) {
        enemy.GetComponent<Lifecycle>().onDeath(addCurrency);
    }

    private void addCurrency(GameObject obj) {
        currentCurrency += obj.GetComponent<CurrencyDrop>().getCurrencyAmount();
        currencyAmountChangedEvent?.Invoke(currentCurrency);
    }

    public static void reduceCurrency(int value) {
        currentCurrency -= value;
        currencyAmountChangedEvent?.Invoke(currentCurrency);
    }
    public static int getCurrencyAmount() {
        return currentCurrency;
    }
}

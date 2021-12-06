using Unbreakable.UI;
using UnityEngine;

namespace Unbreakable {
    public class GameManager : MonoBehaviour {
        public static bool isGamePaused;
    
        [SerializeField] private FloatVariable currencyAmount;
        [SerializeField] private PauseUIController pauseUIController;
        [SerializeField] private PlayerPrefsManager playerPrefsManager;
        
        private WaveManager waveManager;
        
        private void Awake() {
            currencyAmount.changeValue(0);
            waveManager = GetComponent<WaveManager>();
            waveManager.waveEndedEvent += saveScore;
        }

        private void saveScore() {
            playerPrefsManager.setLastClearedWaveIndex(Mathf.Max(playerPrefsManager.getLastClearedWaveIndex(), waveManager.waveIndex));
        }

        private void Update() {
            if (Input.GetButtonDown("Pause")) {
                if(isGamePaused) return;
                isGamePaused = true;
                pauseUIController.displayPauseUI();
                Time.timeScale = 0;
            }
        }

    }
}

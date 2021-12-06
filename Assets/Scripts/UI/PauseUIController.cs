using TMPro;
using Unbreakable.Gameplay;
using Unbreakable.Gameplay.WaveSystem;
using Unbreakable.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unbreakable.UI {
    public class PauseUIController : MonoBehaviour {
        
        private static readonly int FADE_IN = Animator.StringToHash("FadeIn");
        private static readonly int FADE_OUT = Animator.StringToHash("FadeOut");
        private static readonly int DISPLAY_CONTROLS = Animator.StringToHash("DisplayControls");
        private static readonly int DISPLAY_MENU = Animator.StringToHash("DisplayMenu");
        
        private const string CURRENT_WAVE_TEXT = "Current wave: ";
        private const string LAST_WAVE_TEXT = "Last cleared wave: ";
        private const string SPLASH_SCREEN_SCENE_PATH = "Scenes/SplashScreen";

        [SerializeField] private WaveManager waveManager;
        [SerializeField] private TextMeshProUGUI currentWaveTextMesh;
        [SerializeField] private TextMeshProUGUI lastWaveTextMesh;
        [SerializeField] private PlayerPrefsManager playerPrefsManager;
        
        private Animator pauseUIAnimator;

        private void Awake() {
            pauseUIAnimator = GetComponent<Animator>();
        }
        
        // Used by onClick button event
        public void resume() {
            GameManager.isGamePaused = false;
            pauseUIAnimator.SetTrigger(FADE_OUT);
            Time.timeScale = 1;
        }
        
        // Used by onClick button event
        public void exit() {
            resume();
            SceneManager.LoadScene(SPLASH_SCREEN_SCENE_PATH);
        }
        
        public void displayPauseUI() {
            pauseUIAnimator.SetTrigger(FADE_IN);
            currentWaveTextMesh.text = CURRENT_WAVE_TEXT + waveManager.waveIndex;
            lastWaveTextMesh.text = LAST_WAVE_TEXT + playerPrefsManager.getLastClearedWaveIndex(); 
        }

        public void displayControls() {
            pauseUIAnimator.SetTrigger(DISPLAY_CONTROLS);
        }
    
        public void displayMenu() {
            pauseUIAnimator.SetTrigger(DISPLAY_MENU);
        }
    }
}

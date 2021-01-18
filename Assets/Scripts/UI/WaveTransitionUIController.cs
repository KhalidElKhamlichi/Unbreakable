using TMPro;
using UnityEngine;

namespace Unbreakable.UI {
    public class WaveTransitionUIController : MonoBehaviour
    {
        private static readonly int FADE_IN = Animator.StringToHash("FadeIn");

        [SerializeField] private TextMeshProUGUI transitionText;

        private Animator waveTransitionCanvasAnimator;

        private void Awake() {
            subscribeToGameEvents();
        }

        void Start() {
            waveTransitionCanvasAnimator = GetComponent<Animator>();
            transitionText.text = $"WAVE {WaveManager.waveIndex} COMPLETE";
        }

        private void subscribeToGameEvents() {
            WaveManager.waveEndedEvent += displayTransitionUI;
        }

        private void displayTransitionUI() {
            transitionText.text = $"WAVE {WaveManager.waveIndex} COMPLETE";
            Invoke(nameof(fadeInWaveTransition), .4f);
        }

        private void fadeInWaveTransition() {
            waveTransitionCanvasAnimator.SetTrigger(FADE_IN);
        }

        private void OnDestroy() {
            unsubscribeToGameEvents();
        }
    
        private void unsubscribeToGameEvents() {
            WaveManager.waveEndedEvent -= displayTransitionUI;
        }
    }
}

using TMPro;
using Unbreakable.Gameplay.WaveSystem;
using UnityEngine;

namespace Unbreakable.UI {
    public class WaveTransitionUIController : MonoBehaviour
    {
        private static readonly int FADE_IN = Animator.StringToHash("FadeIn");
        private const string WAVE_COMPLETE_TEXT = "WAVE {0} COMPLETE";

        [SerializeField] private WaveManager waveManager;
        [SerializeField] private TextMeshProUGUI transitionText;
        [SerializeField] private float delayBeforeTransition;

        private Animator waveTransitionCanvasAnimator;

        private void Awake() {
            waveManager.waveEndedEvent += displayTransitionUI;
        }

        void Start() {
            waveTransitionCanvasAnimator = GetComponent<Animator>();
        }

        private void displayTransitionUI() {
            transitionText.text = string.Format(WAVE_COMPLETE_TEXT, waveManager.waveIndex);
            Invoke(nameof(fadeInWaveTransition), delayBeforeTransition);
        }

        private void fadeInWaveTransition() {
            waveTransitionCanvasAnimator.SetTrigger(FADE_IN);
        }

        private void OnDestroy() {
            waveManager.waveEndedEvent -= displayTransitionUI;
        }

    }
}

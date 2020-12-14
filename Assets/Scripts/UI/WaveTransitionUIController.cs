﻿using TMPro;
using UnityEngine;

public class WaveTransitionUIController : MonoBehaviour
{
        private static readonly int FADE_IN = Animator.StringToHash("FadeIn");

        [SerializeField] private TextMeshProUGUI transitionText;
//        [SerializeField] Audio lvlClearAudio;
//        [SerializeField] Audio transitionAudio;
//        [SerializeField] GameObject audioPlayer;

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
        
        private void playLvlClearAudio() {
//            Instantiate(audioPlayer).GetComponent<AudioPlayer>().play(lvlClearAudio);
        }

        private void displayTransitionUI() {
            transitionText.text = $"WAVE {WaveManager.waveIndex} COMPLETE";
            Invoke(nameof(fadeInWaveTransition), .4f);
        }

        private void fadeInWaveTransition() {
            waveTransitionCanvasAnimator.SetTrigger(FADE_IN);
//            Instantiate(audioPlayer).GetComponent<AudioPlayer>().play(transitionAudio);
        }

}

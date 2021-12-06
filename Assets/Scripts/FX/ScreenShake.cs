using Cinemachine;
using UnityEngine;

namespace Unbreakable.FX {
    public class ScreenShake : MonoBehaviour {
        
        private float shakeAmplitude = 1.2f;         
        private float shakeFrequency = 2.0f;         
    
        private bool isShaking;
        private float shakeAmount;
        private float shakeDuration;

        private float shakeElapsedTime;
        private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
        void Start () {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            if (virtualCamera != null)
                virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        }
        public void shake(float shakeAmount, float shakeDuration) {
            if (isShaking) return;
            shakeElapsedTime = shakeDuration;
            shakeAmplitude = shakeAmount;
            isShaking = true;
        }
    
        private void Update() {
            if (virtualCamera != null && virtualCameraNoise != null) {
                if (shakeElapsedTime > 0) {
                    virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                    virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                    shakeElapsedTime -= Time.deltaTime;
                }
                else {
                    isShaking = false;
                    virtualCameraNoise.m_AmplitudeGain = 0f;
                    shakeElapsedTime = 0f;
                }
            }
        }
    }
}

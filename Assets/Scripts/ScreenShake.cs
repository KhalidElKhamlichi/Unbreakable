using Cinemachine;
using UnityEngine;

namespace Unbreakable {
    public class ScreenShake : MonoBehaviour {
    
//    private Transform mainCamTransform;
//    private CameraFollow cameraFollow;
        private float shakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
        private float shakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter
    
        private bool isShaking;
        private float shakeAmount;
        private float shakeDuration;

        private float shakeElapsedTime;
        // Cinemachine Shake
        private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
        void Start () {
//        mainCamTransform = GetComponent<CinemachineVirtualCamera>().Follow.transform;
//        cameraFollow = GetComponent<CameraFollow>();
            // Get Virtual Camera Noise Profile
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            if (virtualCamera != null)
                virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        }
        public void shake(float shakeAmount, float shakeDuration) {
            if (isShaking) return;
            shakeElapsedTime = shakeDuration;
            shakeAmplitude = shakeAmount;
            isShaking = true;
//        this.shakeAmount = shakeAmount;
//        this.shakeDuration = shakeDuration;
//        cameraFollow.enabled = false;
//        isShaking = true;
//        InvokeRepeating(nameof(beginShake), 0, Time.fixedDeltaTime);
//        StartCoroutine(stopShake());
        }
    
        void Update() {
            // If the Cinemachine componet is not set, avoid update
            if (virtualCamera != null && virtualCameraNoise != null)
            {
                // If Camera Shake effect is still playing
                if (shakeElapsedTime > 0) {
                    // Set Cinemachine Camera Noise parameters
                    virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                    virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                    // Update Shake Timer
                    shakeElapsedTime -= Time.deltaTime;
                }
                else {
                    isShaking = false;
                    // If Camera Shake effect is over, reset variables
                    virtualCameraNoise.m_AmplitudeGain = 0f;
                    shakeElapsedTime = 0f;
                }
            }
        }
    
    
    
//    private void beginShake() {
//        if(shakeAmount > 0) {
//            Vector3 camPos = mainCamTransform.position;
//
//            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
//            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
//            camPos.x += offsetX;
//            camPos.y += offsetY;
//            mainCamTransform.position = camPos;
//        }
//    }
//
//    private IEnumerator stopShake() {
//        yield return new WaitForSeconds(shakeDuration);
//        cameraFollow.enabled = true;
//        isShaking = false;
//        CancelInvoke(nameof(beginShake));
//    }
    }
}

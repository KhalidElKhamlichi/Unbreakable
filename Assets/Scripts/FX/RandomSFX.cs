using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class RandomSFX : MonoBehaviour {
    [SerializeField] private AudioClip[] audioClips;
    void OnEnable() {
        int index = Random.Range(0, audioClips.Length);
        GetComponent<AudioSource>().PlayOneShot(audioClips[index]);
    }

}

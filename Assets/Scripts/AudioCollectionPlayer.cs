using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollectionPlayer : MonoBehaviour {
    public List<AudioClip> audioClips;
    private AudioSource audioSource;
    
    private void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    
    public void playRandom() {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.PlayOneShot(audioSource.clip);
    }
}

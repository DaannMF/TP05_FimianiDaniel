using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource loopSource;

    // Start is called before the first frame update
    void Start() {
        this.introSource.Play();
        this.loopSource.PlayScheduled(AudioSettings.dspTime + this.introSource.clip.length);
    }
}

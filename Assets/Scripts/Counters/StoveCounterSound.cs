using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

    [SerializeField] StoveCounter stoveCounter;

    private AudioSource audioSource;

    private bool cooking;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += (sender, e) => {
            if (stoveCounter.IsCooking())
                audioSource.Play();
            else
                audioSource.Pause();
        };
    }
}

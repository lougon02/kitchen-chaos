using System;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject {
    [Serializable]
    private struct AudioClipRefs {
        public string name;
        public AudioClip[] audioClips;
    }

    [SerializeField] private AudioClipRefs[] audioClipRefs;

    public bool TryGetAudioClips(string name, out AudioClip[] audioClips) {
        // If dictionaries were serializable, this would have O(1) access instead of O(n), but this should be fine for a small game.
        foreach (AudioClipRefs audioClipRef in audioClipRefs) {
            if (audioClipRef.name == name) {
                audioClips = audioClipRef.audioClips;
                return true;
            }
        }

        Debug.LogError("AudioClip not found: " + name);
        audioClips = null;
        return false;
    }
}

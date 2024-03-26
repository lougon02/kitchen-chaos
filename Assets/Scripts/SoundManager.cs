using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private const string PLAYER_PREFS_SFX_VOLUME = "SfxVolume";

    public static SoundManager Instance { get; private set; }

    private float volume = 1f;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }

    private void Start() {
        // inline function
        DeliveryManager.Instance.OnDeliveryCompleted +=
            (sender, e) => PlaySound("DeliverySuccess", DeliveryCounter.Instance.transform.position);
        DeliveryManager.Instance.OnDeliveryFailed +=
            (sender, e) => PlaySound("DeliveryFail", DeliveryCounter.Instance.transform.position);
        CuttingCounter.OnAnyCut +=
            (sender, e) => PlaySound("Chop", (sender as CuttingCounter).transform.position);
        Player.Instance.OnPickedObject +=
            (sender, e) => PlaySound("ObjectPickup", Player.Instance.transform.position);
        BaseCounter.OnAnyObjectPlaced +=
            (sender, e) => PlaySound("ObjectDrop", (sender as BaseCounter).transform.position);
        TrashCounter.OnAnyObjectTrashed +=
            (sender, e) => PlaySound("Trash", (sender as TrashCounter).transform.position);
    }


    public void PlaySound(string audioClipName, Vector3 position, float volumeMultiplier = 1f) {
        if (audioClipRefsSO.TryGetAudioClips(audioClipName, out AudioClip[] audioClips)) 
            AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volumeMultiplier * volume);

    }

    public void ChangeVolume() {
        volume += 0.1f;
        if (volume > 1f) volume = 0f;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }

}

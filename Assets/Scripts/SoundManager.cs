using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    public static SoundManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
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


    public void PlaySound(string audioClipName, Vector3 position, float volume = 1f) {
        if (audioClipRefsSO.TryGetAudioClips(audioClipName, out AudioClip[] audioClips)) 
            AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);

    }

}

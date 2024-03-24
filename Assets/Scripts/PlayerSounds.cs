using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {


    private Player player;
    private float footstepInterval = .3f;
    private float footstepTime;

    private void Awake() {
        player = gameObject.GetComponent<Player>();
        footstepTime = footstepInterval;
    }


    private void Update() {
        footstepTime += Time.deltaTime;

        if (footstepTime >= footstepInterval && player.IsWalking()) {
            SoundManager.Instance.PlaySound("Footstep", player.transform.position, 1f);
        }
    }
}

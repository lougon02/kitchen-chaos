using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    [SerializeField] private Player player;
    private Animator animator;
    private const string IS_WALKLING = "IsWalking";

    void Awake() {
        animator = GetComponent<Animator>();
    }
    void Update() {
        animator.SetBool(IS_WALKLING, player.IsWalking());
    }
}

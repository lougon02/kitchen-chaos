using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;

    private void Update() {
        Vector2 moveVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(moveVector.x, 0, moveVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }
}

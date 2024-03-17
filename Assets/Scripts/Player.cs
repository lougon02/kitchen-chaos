using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;

    private bool isWalking = false;

    private void Update() {
        Vector2 moveVector = new Vector2(0,0);
        if (Input.GetKey(KeyCode.W)) {
            moveVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            moveVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            moveVector.x = 1;
        }

        moveVector.Normalize();

        Vector3 moveDir = new Vector3(moveVector.x, 0, moveVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }
}

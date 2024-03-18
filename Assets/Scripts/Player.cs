using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;

    private void Update() {
        Vector2 moveVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(moveVector.x, 0, moveVector.y);

        float playerHeight = 2f;
        float playerRadius = .7f;
        float moveDist = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDist);

        if (!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            moveDirX.Normalize();
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDist);
            
            if (canMove) moveDir = moveDirX;
            else {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                moveDirZ.Normalize();
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDist);
                
                if (canMove) moveDir = moveDirZ;
            }
        } 
        
        if (canMove) transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }
}

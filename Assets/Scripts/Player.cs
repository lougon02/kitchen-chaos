using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking = false;
    private ClearCounter selectedCounter;

    private void Awake() {
        if (Instance != null) Debug.LogError("There is more than one Player instance in the scene");
        
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteract += GameInput_OnInteract;
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        HandleMovement();
        CheckForInteractable();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void CheckForInteractable() {
        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, countersLayerMask)) {
            if (hit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                if (clearCounter != selectedCounter) 
                    SetSelectedCounter(clearCounter);

            } else SetSelectedCounter(null);
        } else SetSelectedCounter(null);
    }

    private void HandleMovement() {
        Vector2 moveVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(moveVector.x, 0, moveVector.y);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

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
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }
}

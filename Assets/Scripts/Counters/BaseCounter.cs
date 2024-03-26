using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static void ResetStaticData() {
        OnAnyObjectPlaced = null;
    }

    public static event EventHandler OnAnyObjectPlaced;

    [SerializeField] private Transform objectSpawnPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player) { }

    public Transform GetObjectSpawnPoint() {
        return objectSpawnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float spawnPlateTime;
    [SerializeField] private float maxPlates;

    private float plateSpawnTimePassed = 0f;
    private int platesSpawnedAmount = 0;

    private void Update() {
        plateSpawnTimePassed += Time.deltaTime;

        if (plateSpawnTimePassed >= spawnPlateTime) {
            plateSpawnTimePassed = 0f;

            if (platesSpawnedAmount < maxPlates) {
                platesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if(platesSpawnedAmount > 0) {
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        } else {

        }
    }
}

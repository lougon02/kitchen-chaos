using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject here
            if (player.HasKitchenObject()) {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // There is a KitchenObject here
            if (player.HasKitchenObject()) {
                // Player is carrying something

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is carrying plate

                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // Counter has a valid ingredient here
                        GetKitchenObject().DestroySelf();
                    };
                } else {
                    // Player is carrying ingredient 
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // Counter has a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            // Plater has a valid ingredient
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else { 
                GetKitchenObject().SetKitchenObjectParent(player); 
            }
                
        }
    }
}

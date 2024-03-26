using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress {

    public static EventHandler OnAnyCut;

    public static void ResetStaticData() {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress = 0;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipe(player.GetKitchenObject().GetKitchenObjectSO())) {
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(player.GetKitchenObject().GetKitchenObjectSO());

                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progress = (float) cuttingProgress / cuttingRecipeSO.cutsNeeded
                    });
                }
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is carrying plate

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // Counter has a valid ingredient here
                        GetKitchenObject().DestroySelf();
                    };
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

            if (cuttingRecipeSO == null) return;

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progress = (float)cuttingProgress / cuttingRecipeSO.cutsNeeded
            });

            if (cuttingProgress < cuttingRecipeSO.cutsNeeded) return;

            KitchenObjectSO cutKitchenObjectSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

            if (cutKitchenObjectSO == null) return;


            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }

    private bool HasRecipe(KitchenObjectSO input) {
        return GetCuttingRecipeSOFromInput(input) != null;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(input);

        if (cuttingRecipeSO == null) return null;

        return cuttingRecipeSO.output;
    }

    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}

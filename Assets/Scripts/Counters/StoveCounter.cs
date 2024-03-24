using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private FryingRecipeSO fryingRecipeSO;
    private float fryingTimer = 0;
    private bool isCooking = false;

    private void Update() {
        if (HasKitchenObject() && fryingRecipeSO != null) {
            // Frying
            fryingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progress = fryingTimer / fryingRecipeSO.timeNeeded
            });

            if (fryingTimer > fryingRecipeSO.timeNeeded) {
                //Fried
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                fryingTimer = 0f;
                ChangeActiveRecipe(GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO()));
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipe(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    ChangeActiveRecipe(GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO()));
                }
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is carrying plate

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // Counter has a valid ingredient here
                        GetKitchenObject().DestroySelf();

                        ChangeActiveRecipe(null);

                        fryingTimer = 0f;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progress = 0f
                        });
                    };
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
                ChangeActiveRecipe(null);

                fryingTimer = 0f;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progress = 0f
                });
            }
        }
    }

    private void ChangeActiveRecipe(FryingRecipeSO newFryingRecipeSO) {
        if((fryingRecipeSO == null) ^ (newFryingRecipeSO == null)) {
            // State Changed
            isCooking = !isCooking;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }

        fryingRecipeSO = newFryingRecipeSO;
    }

    private bool HasRecipe(KitchenObjectSO input) {
        return GetFryingRecipeSOFromInput(input) != null;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromInput(input);

        if (fryingRecipeSO == null) return null;

        return fryingRecipeSO.output;
    }

    private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO input) {
        foreach (FryingRecipeSO FryingRecipeSO in fryingRecipeSOArray) {
            if (FryingRecipeSO.input == input) {
                return FryingRecipeSO;
            }
        }

        return null;
    }

    public bool IsCooking() {
        return isCooking;
    }
}

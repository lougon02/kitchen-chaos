using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnDeliveryCompleted;
    public event EventHandler OnDeliveryFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeList;
    [SerializeField] private float recipeSpawnTime = 4f;
    [SerializeField] private int waitingRecipesMax = 4;

    private List<RecipeSO> waitingRecipeList = new List<RecipeSO>();
    private float spawnRecipeTimer;
    private int correctRecipesDelivered;
    
    private void Awake() {
        Instance = this;

        // To spawn one immediatly
        spawnRecipeTimer = recipeSpawnTime;
    }

    private void Update() {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= recipeSpawnTime) {
            spawnRecipeTimer = 0f;

            if (waitingRecipeList.Count >= waitingRecipesMax) return;

            RecipeSO waitingRecipeSO = recipeList.recipeSOList[UnityEngine.Random.Range(0, recipeList.recipeSOList.Count)];
            waitingRecipeList.Add(waitingRecipeSO);

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        foreach (RecipeSO waitingRecipeSO in waitingRecipeList) {
            // For all recipes
            bool isCorrectRecipe = true;
            if (waitingRecipeSO.ingredientSOList.Count == plateKitchenObject.GetIngredientsList().Count) {
                // Has same number of ingredients
                foreach (KitchenObjectSO ingredient in plateKitchenObject.GetIngredientsList()) {
                    // For all ingredients in the plate
                    if (!waitingRecipeSO.ingredientSOList.Contains(ingredient)) {
                        // Ingredient not found in recipe, recipe incorrect
                        isCorrectRecipe = false;
                        break;
                    } else {
                        // Ingredient found in recipe
                    }
                }

                if (isCorrectRecipe) {
                    // Recipe found
                    Debug.Log("Correct recipe");
                    correctRecipesDelivered++;
                    waitingRecipeList.Remove(waitingRecipeSO);
                    OnDeliveryCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }

            } else {
                // Has different number of ingredients, recipe incorrect
                continue;
            }
        }
        Debug.Log("Incorrect recipe");
        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeList() {
        return waitingRecipeList;
    }

    public int GetCorrectRecipesDelivered() {
        return correctRecipesDelivered;
    }

}

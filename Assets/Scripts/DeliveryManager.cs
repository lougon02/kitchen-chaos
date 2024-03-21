using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeList;
    [SerializeField] private float recipeSpawnTime = 4f;
    [SerializeField] private int waitingRecipesMax = 4;

    private List<RecipeSO> waitingRecipeList = new List<RecipeSO>();
    private float spawnRecipeTimer = 0f;
    
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= recipeSpawnTime) {
            spawnRecipeTimer = 0f;

            if (waitingRecipeList.Count >= waitingRecipesMax) return;

            RecipeSO waitingRecipeSO = recipeList.recipeSOList[Random.Range(0, recipeList.recipeSOList.Count)];
            waitingRecipeList.Add(waitingRecipeSO);
            Debug.Log(waitingRecipeSO.recipeName);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        foreach (RecipeSO waitingRecipeSO in waitingRecipeList) {
            // For all recipes
            if(waitingRecipeSO.ingredientSOList.Count == plateKitchenObject.GetIngredientsList().Count) {
                // Has same number of ingredients
                foreach(KitchenObjectSO ingredient in plateKitchenObject.GetIngredientsList()) {
                    // For all ingredients in the plate
                    if (!waitingRecipeSO.ingredientSOList.Contains(ingredient)) {
                        // Ingredient not found in recipe, recipe incorrect
                        Debug.Log("Incorrect recipe");
                        return;
                    } else {
                        // Ingredient found in recipe
                    }
                }
            } else {
                // Has different number of ingredients, recipe incorrect
                Debug.Log("Incorrect recipe");
                return;
            }
        }

        // Recipe is correct
        Debug.Log("Correct recipe");
    }

}

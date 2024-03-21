using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliveryCompleted += DeliveryManager_OnDeliveryCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManager_OnDeliveryCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) { 
            if (child != recipeTemplate) {
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeList()) {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerTemplateUI>().SetRecipeSO(recipeSO);
        }
    }

}

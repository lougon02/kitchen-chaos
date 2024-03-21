using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconTemplateUI : MonoBehaviour
{

    [SerializeField] private Image image;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
        image.sprite = kitchenObjectSO.sprite;
    }
}

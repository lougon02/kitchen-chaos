using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] public KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

}

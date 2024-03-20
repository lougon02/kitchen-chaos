using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private bool show = false;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, System.EventArgs e) {
        show = !show;
        
        stoveOnGameObject.SetActive(show);
        particlesGameObject.SetActive(show);
    }
}

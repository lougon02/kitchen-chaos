using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject objWithProgress;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start() {
        hasProgress = objWithProgress.GetComponent<IHasProgress>();
        if (hasProgress == null)
            Debug.LogError("Game Object " + objWithProgress + " does not have a component that implements IHasProgress");
        hasProgress.OnProgressChanged += ObjWithProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void ObjWithProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progress;

        if (e.progress == 0f || e.progress >= 1f) Hide();
        else Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

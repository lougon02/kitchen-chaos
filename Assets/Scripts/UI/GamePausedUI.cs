using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => KitchenGameManager.Instance.TogglePauseGame());
        mainMenuButton.onClick.AddListener(() => Loader.LoadScene(Loader.Scene.MainMenuScene));
    }

    private void Start() {
        KitchenGameManager.Instance.OnGamePaused += (sender, e) => Show();
        KitchenGameManager.Instance.OnGameUnpaused += (sender, e) => Hide();
        Hide();
    }


    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

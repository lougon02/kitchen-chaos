using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Start() {
        timerImage.fillAmount = 1f;
    }

    private void Update() {
        float timerValue = 1 - KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
        timerImage.fillAmount = timerValue;
    }
}

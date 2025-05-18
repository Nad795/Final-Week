using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private GameObject endingPanel;
    [SerializeField] private TextMeshProUGUI endingTitleText;
    [SerializeField] private TextMeshProUGUI endingDescText;

    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private Slider stressBar;
    [SerializeField] private TextMeshProUGUI stressText;

    public void UpdateUI()
    {
        dayText.text = "Day " + playerStatus.day;
        timeText.text = "Time Left: " + playerStatus.timeLeft + "h";

        progressBar.value = playerStatus.progress;
        staminaBar.value = playerStatus.stamina;
        stressBar.value = playerStatus.stress;

        progressText.text = "Progress: " + progressBar.value + "%";
        staminaText.text = "Stamina: " + staminaBar.value + "%";
        stressText.text = "Stress: " + stressBar.value + "%";
    }

    public void ShowEndingUI(Ending ending)
    {
        playerMovement.canMove = false;
        endingPanel.SetActive(true);
        endingTitleText.text = ending.endingTitle;
        endingDescText.text = ending.endingDescription;
    }
}

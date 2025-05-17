using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
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
        dayText.text = "Day " + player.day;
        timeText.text = "Time Left: " + player.timeLeft + "h";

        progressBar.value = player.progress;
        staminaBar.value = player.stamina;
        stressBar.value = player.stress;

        progressText.text = "Progress: " + progressBar.value + "%";
        staminaText.text = "Stamina: " + staminaBar.value + "%";
        stressText.text = "Stress: " + stressBar.value + "%";
    }

    public void ShowEndingUI(Ending ending)
    {
        endingPanel.SetActive(true);
        endingTitleText.text = ending.endingTitle;
        endingDescText.text = ending.endingDescription;
    }
}

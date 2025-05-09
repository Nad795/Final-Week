using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private TMP_Text endingTitleText;
    [SerializeField] private TMP_Text endingDescText;

    void Start()
    {
        endingPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        dayText.text = "Day: " + player.day;
        timeText.text = "Time Left: " + player.timeLeft + "h";
    }

    public void ShowEndingUI(Ending ending)
    {
        endingPanel.SetActive(true);
        endingTitleText.text = ending.endingTitle;
        endingDescText.text = ending.endingDescription;
    }
}

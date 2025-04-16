using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private TextMeshProUGUI endingTitleText;
    [SerializeField] private TextMeshProUGUI endingDescText;

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

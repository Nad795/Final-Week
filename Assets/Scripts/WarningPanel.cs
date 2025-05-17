using UnityEngine;
using TMPro;

public class WarningPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI stressText;

    private Activity currentActivity;

    public void Show(Activity activity)
    {
        currentActivity = activity;

        nameText.text = activity.activityName;
        timeText.text = $"Time Cost: {activity.timeCost}";
        progressText.text = $"Progress: {activity.progressChange}";
        staminaText.text = $"Stamina: {activity.staminaChange}";
        stressText.text = $"Stress: {activity.stressChange}";

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public Activity GetCurrentActivity()
    {
        return currentActivity;
    }

    public bool IsVisible()
    {
        return panel.activeSelf;
    }
}

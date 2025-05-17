using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Activity activity;

    private bool isPlayerInRange = false;
    private ActivityManager activityManager;
    private WarningPanel warningPanel;

    private void Start()
    {
        activityManager = FindAnyObjectByType<ActivityManager>();
        warningPanel = GetComponentInChildren<WarningPanel>(true);

        if (activityManager == null)
        {
            Debug.LogError("ActivityManager tidak ditemukan di scene!");
        }

        if (warningPanel == null)
        {
            Debug.LogError("WarningPanel tidak ditemukan di scene!");
        }
    }

    public void Interact()
    {
        if (isPlayerInRange && activity != null && warningPanel.IsVisible())
        {
            activityManager.DoActivity(activity);
            warningPanel.Hide();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            warningPanel.Show(activity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            warningPanel.Hide();
        }
    }
}
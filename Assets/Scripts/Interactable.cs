using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Activity activity;

    private bool isPlayerInRange = false;
    private ActivityManager activityManager;

    private void Start()
    {
        activityManager = FindAnyObjectByType<ActivityManager>();

        if (activityManager == null)
        {
            Debug.LogError("ActivityManager tidak ditemukan di scene!");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (activity != null && activityManager != null)
            {
                activityManager.DoActivity(activity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
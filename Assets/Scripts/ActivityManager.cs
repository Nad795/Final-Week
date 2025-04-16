using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;

    public void DoActivity(Activity activity)
    {
        if(player.timeLeft <= activity.timeCost)
        {
            player.timeLeft -= activity.timeCost;
            player.progress += activity.progressChange;
            player.stamina += activity.staminaChange;
            player.stress += activity.stressChange;

            Debug.Log("Melakukan {activity.activityName}");
        }
        else
        {
            Debug.Log("Waktu tidak cukup");
        }
    }
}

using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField] private EventManager eventManager;

    public void DoActivity(Activity activity)
    {
        if(player.timeLeft >= activity.timeCost)
        {
            player.timeLeft -= activity.timeCost;
            player.progress += activity.progressChange;
            player.stamina += activity.staminaChange;
            player.stress += activity.stressChange;

            if (activity.activityName == "Study")
            {
                // quizManager.StartQuiz();
            }
            else if (activity.activityName == "Chat")
            {
                eventManager.TriggerRandomEvent();
            }
            else
            {
                player.progress += activity.progressChange;
            }

            Debug.Log($"Melakukan {activity.activityName}");
        }
        else
        {
            Debug.Log("Waktu tidak cukup");
        }
    }
}

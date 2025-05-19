using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;

    private Activity currentActivity;

    public void DoActivity(Activity activity)
    {
        if (player.timeLeft < activity.timeCost)
        {
            return;
        }

        if (activity.activityName == "Study")
        {
            currentActivity = activity;
            quizManager.StartQuiz(OnQuizCompleted);
        }
        else
        {
            ApplyEffect(activity);
            if (activity.activityName == "Chat")
            {
                eventManager.TriggerRandomEvent();
            }
        }
    }

    private void ApplyEffect(Activity activity)
    {
        player.timeLeft -= activity.timeCost;
        player.progress = Mathf.Clamp(player.progress + activity.progressChange, 0, 100);
        player.stamina = Mathf.Clamp(player.stamina + activity.staminaChange, 0, 100);
        player.stress = Mathf.Clamp(player.stress + activity.stressChange, 0, 100);

        uiManager.UpdateUI();

        if (player.timeLeft <= 0 || player.stress >= 100)
        {
            gameManager.EndDay();
        }
    }

    private void OnQuizCompleted(bool isCorrect)
    {
        if (isCorrect)
        {
            ApplyEffect(currentActivity);
        }
        else
        {
            player.timeLeft -= currentActivity.timeCost;
            player.stamina = Mathf.Clamp(player.stamina + currentActivity.staminaChange, 0, 100);
            player.stress = Mathf.Clamp(player.stress + currentActivity.stressChange, 0, 100);
            uiManager.UpdateUI();
            
            if (player.timeLeft <= 0 || player.stress >= 100)
            {
                gameManager.EndDay();
            }
        }

        currentActivity = null;
    }
}

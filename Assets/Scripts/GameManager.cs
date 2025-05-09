using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField] private ActivityManager activity;
    [SerializeField] private UIManager ui;
    [SerializeField] private QuizManager quiz;

    [Header("Ending")]
    [SerializeField] private Ending burnout;
    [SerializeField] private Ending failed;
    [SerializeField] private Ending passed;
    [SerializeField] private Ending excellent;

    public void EndDay()
    {
        player.day++;
        player.timeLeft = 10;    
        if(player.stress > 50)
        {
            player.stamina = 80;
        }
        else
        {
            player.stamina = 100;
        }
        player.stress = 0;

        quiz.ResetDailyQuestions();

        if(player.day > 7)
        {
            ShowEnding();
        }
        else
        {
            ui.UpdateUI();
        }
    }

    public void ShowEnding()
    {
        if(player.IsBurnout)
        {
            ui.ShowEndingUI(burnout);
        }
        else if(player.IsPassed)
        {
            if(player.IsExcellent)
            {
                ui.ShowEndingUI(excellent);
            }
            else
            {
                ui.ShowEndingUI(passed);
            }
        }
        else
        {
            ui.ShowEndingUI(failed);
        }
    }
}

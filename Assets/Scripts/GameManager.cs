using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField] private ActivityManager activity;
    [SerializeField] private UIManager ui;
    [SerializeField] private QuizManager quiz;

    [SerializeField] private GameObject endingPanel;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private GameObject[] warningPanels;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject menuPanel;



    [Header("Ending")]
    [SerializeField] private Ending burnout;
    [SerializeField] private Ending failed;
    [SerializeField] private Ending passed;
    [SerializeField] private Ending excellent;

    void Start()
    {
        endingPanel.SetActive(false);
        quizPanel.SetActive(false);
        eventPanel.SetActive(false);
        fadePanel.SetActive(false);
        menuPanel.SetActive(true);

        foreach (var panel in warningPanels)
        {
            panel.SetActive(false);
        }

        ui.UpdateUI();
    }

    public void EndDay()
    {
        StartCoroutine(HandleEndDay());
    }

    public IEnumerator ShowEnding()
    {
        fadePanel.SetActive(false);
        if (player.IsBurnout)
        {
            ui.ShowEndingUI(burnout);
        }
        else if (player.IsPassed)
        {
            if (player.IsExcellent)
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

        yield return new WaitForSeconds(5f);

        player.ResetStatus();
        ui.UpdateUI();
        menuPanel.SetActive(true);
    }
    
    private IEnumerator HandleEndDay()
    {
        fadePanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        player.day++;
        player.timeLeft = 10;

        if (player.stress > 50)
        {
            player.stamina = 80;
        }
        else
        {
            player.stamina = 100;
        }
        player.stress = 0;

        quiz.ResetDailyQuestions();

        if (player.day > 7 || player.stress >= 100)
        {
            StartCoroutine(ShowEnding());
            yield break;
        }
        ui.UpdateUI();

        fadePanel.SetActive(false);
    }
}

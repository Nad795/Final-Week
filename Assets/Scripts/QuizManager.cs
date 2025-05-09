using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<Question> questions;
    [SerializeField] private PlayerStatus player;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private TMP_Text questionText; 
    [SerializeField] private List<Button> optionButtons;

    private Question currentQuestion;
    private int currectSelection = 0;
    private bool quizActive = false;
    private List<Question> dailyQuestions;
    private List<string> currentOptions;
    private int correctAnswerIndexAfterShuffle;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    private void Start()
    {
        ResetDailyQuestions();
    }
    void Update()
    {
        if(!quizActive) return;

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelection(-1);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelection(1);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            SubmitAnswer();
        }
    }

    public void StartQuiz()
    {
        if (dailyQuestions.Count == 0)
        {
            Debug.Log("Tidak ada soal tersisa untuk hari ini!");
            return;
        }

        quizPanel.SetActive(true);
        quizActive = true;
        currentQuestion = GetRandomQuestion();
        currectSelection = 0;
        questionText.text = currentQuestion.questionText;

        for(int i = 0; i < optionButtons.Count; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];   
        }

        SetupShuffledOptions();
        UpdateSelectionVisual();
    }

    private Question GetRandomQuestion()
    {
        int index = Random.Range(0, dailyQuestions.Count);
        Question selected = dailyQuestions[index];
        dailyQuestions.RemoveAt(index);
        return selected;
    }

    public void ResetDailyQuestions()
    {
        dailyQuestions = new List<Question>(questions);
    }

    private void MoveSelection(int direction)
    {
        currectSelection += direction;

        if(currectSelection < 0)
        {
            currectSelection = optionButtons.Count - 1;
        }
        else if(currectSelection >= optionButtons.Count)
        {
            currectSelection = 0;
        }
        UpdateSelectionVisual();
    }

    private void UpdateSelectionVisual()
    {
        for(int i = 0; i < optionButtons.Count; i++)
        {
            var colors = optionButtons[i].colors;
            colors.normalColor = (i == currectSelection) ? selectedColor : normalColor;
            optionButtons[i].colors = colors;
        }
    }

    public void SubmitAnswer()
    {
        if(currectSelection == correctAnswerIndexAfterShuffle)
        {
            player.progress += 10;
            Debug.Log("YEYY BENARR!");
        }
        else
        {
            Debug.Log("YAHH SALAHH");
        }
        quizPanel.SetActive(false);
        quizActive = false;
    }

    private void SetupShuffledOptions()
    {
        currentOptions = new List<string>(currentQuestion.options);
        string correctAnswer = currentQuestion.options[currentQuestion.correctAnswerIndex];

        for(int i = 0; i < currentOptions.Count; i++)
        {
            int randomIndex = Random.Range(0, currentOptions.Count);
            string temp = currentOptions[i];
            currentOptions[i] = currentOptions[randomIndex];
            currentOptions[randomIndex] = temp;
        }

        for(int i = 0; i < optionButtons.Count; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentOptions[i];
        }

        correctAnswerIndexAfterShuffle = currentOptions.IndexOf(correctAnswer);
    }
}

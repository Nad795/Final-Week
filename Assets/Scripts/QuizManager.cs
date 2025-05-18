using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<Question> questions;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private TextMeshProUGUI questionText; 
    [SerializeField] private List<Button> optionButtons;

    private Question currentQuestion;
    private int currentSelection = 0;
    private bool quizActive = false;
    private List<Question> dailyQuestions;
    private List<string> currentOptions;
    private int correctAnswerIndexAfterShuffle;
    private System.Action<bool> onQuizCompleted;


    public Sprite normalButton;
    public Sprite selectedButton;

    private void Start()
    {
        ResetDailyQuestions();
    }
    void Update()
    {
        if(!quizActive) return;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelection(-1);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelection(1);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            SubmitAnswer();
        }
    }

    public void StartQuiz(System.Action<bool> callback)
    {
        onQuizCompleted = callback;
        playerMovement.canMove = false;
        if (dailyQuestions.Count == 0)
        {
            return;
        }

        quizPanel.SetActive(true);
        quizActive = true;
        currentQuestion = GetRandomQuestion();
        currentSelection = 0;
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
        currentSelection += direction;

        if(currentSelection < 0)
        {
            currentSelection = optionButtons.Count - 1;
        }
        else if(currentSelection >= optionButtons.Count)
        {
            currentSelection = 0;
        }
        UpdateSelectionVisual();
    }

    private void UpdateSelectionVisual()
    {
        for (int i = 0; i < optionButtons.Count; i++)
        {
            var image = optionButtons[i].GetComponent<Image>();
            image.sprite = (i == currentSelection) ? selectedButton : normalButton;
        }
    }

    public void SubmitAnswer()
    {
        bool isCorrect = currentSelection == correctAnswerIndexAfterShuffle;
        if (isCorrect)
        {
            Debug.Log("YEYY BENARR!");
        }
        else
        {
            Debug.Log("YAHH SALAHH");
        }
        quizPanel.SetActive(false);
        quizActive = false;
        playerMovement.canMove = true;
        onQuizCompleted?.Invoke(isCorrect);
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class EventManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private TextMeshProUGUI eventTitle;
    [SerializeField] private TextMeshProUGUI eventDescription;
    [SerializeField] private List<RandomEvent> events;
    [SerializeField] private List<Button> optionButtons;

    private int currentSelection = 0;
    private bool eventActive = false;
    private RandomEvent currentEvent;
    private System.Action deferredEffect;
    private bool isClosing = false;


    public Sprite normalButton;
    public Sprite selectedButton;

    public void TriggerRandomEvent()
    {
        if (events.Count == 0) return;

        int index = UnityEngine.Random.Range(0, events.Count);
        RandomEvent randomEvent = events[index];
        currentEvent = randomEvent;

        eventTitle.text = currentEvent.randomEventName;
        eventDescription.text = currentEvent.randomEventDescription;

        playerMovement.canMove = false;
        eventActive = true;
        eventPanel.SetActive(true);

        foreach (var btn in optionButtons)
        {
            btn.gameObject.SetActive(randomEvent.hasChoice);
        }

        if (currentEvent.hasChoice)
        {
            currentSelection = 0;
            UpdateSelectionVisual();
            deferredEffect = () => ChoiceEffect(currentEvent, currentSelection == 0);
        }
        else
        {
            deferredEffect = () => EventEffect(currentEvent);
            StartCoroutine(CloseEventPanelAfterDelay(2f));
            Debug.Log("Random Event: " + randomEvent.name);
        }
    }


    public void EventEffect(RandomEvent ev)
    {
        playerStatus.timeLeft -= ev.timeCost;
        playerStatus.progress = Mathf.Clamp(playerStatus.progress + ev.progressChange, 0, 100);
        playerStatus.stamina = Mathf.Clamp(playerStatus.stamina + ev.staminaChange, 0, 100);
        playerStatus.stress = Mathf.Clamp(playerStatus.stress + ev.stressChange, 0, 100);

        if (playerStatus.timeLeft <= 0)
        {
            gameManager.EndDay();
        }
    }

    public void ChoiceEffect(RandomEvent ev, bool accepted)
    {
        if (!ev.hasChoice) return;

        if (accepted)
        {
            playerStatus.timeLeft -= ev.choiceTimeCost;
            playerStatus.stress = Mathf.Clamp(playerStatus.stress + ev.choiceStressChange, 0, 100);
            playerStatus.stamina = Mathf.Clamp(playerStatus.stamina + ev.choiceStaminaChange, 0, 100);
        }
        else
        {
            EventEffect(ev);
            return;
        }

        if (playerStatus.timeLeft <= 0)
        {
            gameManager.EndDay();
        }
    }

    private void CloseEventPanel()
    {
        if (isClosing) return;
        isClosing = true;

        eventPanel.SetActive(false);
        eventActive = false;
        playerMovement.canMove = true;

        deferredEffect?.Invoke();
        deferredEffect = null;

        uiManager.UpdateUI();

        StartCoroutine(HandleEndDayIfNeeded());
        isClosing = false;
    }

    void Update()
    {
        if (!eventActive) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SubmitAnswer();
        }
    }

    private void MoveSelection(int direction)
    {
        currentSelection += direction;

        if (currentSelection < 0)
        {
            currentSelection = optionButtons.Count - 1;
        }
        else if (currentSelection >= optionButtons.Count)
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
        if (currentEvent == null) return;

        deferredEffect = () => ChoiceEffect(currentEvent, currentSelection == 0);

        CloseEventPanel();
    }

    private IEnumerator CloseEventPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseEventPanel();
    }
    
    private IEnumerator HandleEndDayIfNeeded()
    {
        yield return null;
        if (playerStatus.timeLeft == 0)
        {
            gameManager.EndDay();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private List<RandomEvent> events;
    [SerializeField] private List<Button> optionButtons;

    private int currectSelection = 0;
    private bool eventActive = false;
    private RandomEvent currentEvent;

    public Sprite normalButton;
    public Sprite selectedButton;

    public void TriggerRandomEvent()
    {
        if (events.Count == 0) return;

        int index = Random.Range(0, events.Count);
        RandomEvent randomEvent = events[index];
        currentEvent = randomEvent;

        Debug.Log(currentEvent);

        if (randomEvent.hasChoice)
        {
            playerMovement.canMove = false;
            eventActive = true;
            eventPanel.SetActive(true);
            UpdateSelectionVisual();
            Debug.Log("Butuh pilihan");
        }
        else
        {
            EventEffect(randomEvent);
            Debug.Log("Random Event: " + randomEvent.name);
        }
    }


    public void EventEffect(RandomEvent ev)
    {
        playerStatus.timeLeft -= ev.timeCost;
        playerStatus.progress += ev.progressChange;
        playerStatus.stamina += ev.staminaChange;
        playerStatus.stress += ev.stressChange;
    }

    public void ChoiceEffect(RandomEvent ev, bool accepted)
    {
        if (!ev.hasChoice) return;

        if (accepted)
        {
            playerStatus.timeLeft -= ev.choiceTimeCost;
            playerStatus.stress += ev.choiceStressChange;
            playerStatus.stamina += ev.choiceStaminaChange;
        }
        else
        {
            EventEffect(ev);
        }
    }

    void Update()
    {
        if(!eventActive) return;

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
        currectSelection += direction;

        if (currectSelection < 0)
        {
            currectSelection = optionButtons.Count - 1;
        }
        else if (currectSelection >= optionButtons.Count)
        {
            currectSelection = 0;
        }
        UpdateSelectionVisual();
    }

    private void UpdateSelectionVisual()
    {
        for (int i = 0; i < optionButtons.Count; i++)
        {
            var image = optionButtons[i].GetComponent<Image>();
            image.sprite = (i == currectSelection) ? selectedButton : normalButton;
        }
    }
    
    public void SubmitAnswer()
    {
        ChoiceEffect(currentEvent, currectSelection == 0);
        eventPanel.SetActive(false);
        eventActive = false;
        playerMovement.canMove = true;
    }

}

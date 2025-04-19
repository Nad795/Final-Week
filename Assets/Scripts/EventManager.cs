using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField] private List<RandomEvent> events;

    public void TriggerRandomEvent()
    {
        if (events.Count == 0) return;

        int index = Random.Range(0, events.Count);
        RandomEvent randomEvent = events[index];

        if(randomEvent.hasChoice)
        {
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
        player.timeLeft -= ev.timeCost;
        player.progress += ev.progressChange;
        player.stamina += ev.staminaChange;
        player.stress += ev.stressChange;
    }

    public void ChoiceEffect(RandomEvent ev, bool accepted)
    {
        if(!ev.hasChoice) return;

        if(accepted)
        {
            player.timeLeft -= ev.choiceTimeCost;
            player.stress += ev.choiceStressChange;
            player.stamina += ev.choiceStaminaChange;
        }
        else
        {
            EventEffect(ev);
        }
    }
}

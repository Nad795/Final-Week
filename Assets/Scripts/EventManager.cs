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

        EventEffect(randomEvent);
        Debug.Log("Random Event: " + randomEvent.name);
    }

    private void EventEffect(RandomEvent ev)
    {
        player.timeLeft -= ev.timeCost;
        player.progress += ev.progressChange;
        player.stamina += ev.staminaChange;
        player.stress += ev.stressChange;
    }
}

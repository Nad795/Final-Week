using UnityEngine;

[CreateAssetMenu(fileName = "New Random Event", menuName = "Random Event")]
public class RandomEvent : ScriptableObject
{
    public string randomEventName;
    public int timeCost;
    public int staminaChange;
    public int stressChange;
    public int progressChange;

    public bool hasChoice;
    public int choiceTimeCost;
    public int choiceStaminaChange;
    public int choiceStressChange;
}

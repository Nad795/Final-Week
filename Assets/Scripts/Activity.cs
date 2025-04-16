using UnityEngine;

[CreateAssetMenu(fileName = "New Activity", menuName = "Activity")]
public class Activity : ScriptableObject
{
    public string activityName;
    public int timeCost;
    public int staminaChange;
    public int stressChange;
    public int progressChange;
}

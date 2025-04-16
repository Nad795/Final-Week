using UnityEngine;

[CreateAssetMenu(fileName = "New Ending", menuName = "Ending")]
public class Ending : ScriptableObject
{
    public string endingTitle;
    public string endingDescription;
    public bool isBurnout;
    public bool isPassed;
    public bool isExcellent;
}

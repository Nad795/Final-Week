using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int day = 1;
    public int timeLeft = 10;
    public int stamina = 100;
    public int stress = 0;
    public int progress = 0;

    public bool IsBurnout => stress >= 100;
    public bool IsPassed => progress >= 75;
    public bool IsExcellent => progress >= 90;
}

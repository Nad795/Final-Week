using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Question")]
public class Question : ScriptableObject
{
    [TextArea] public string questionText;
    public string[] options;
    public int correctAnswerIndex;
}

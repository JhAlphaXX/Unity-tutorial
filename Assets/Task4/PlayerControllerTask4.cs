using UnityEngine;  // needed to access MonoBehaviour, Mathf, Random, GameObject, Transform, etc
using TMPro;        // needed to access TextMeshPro text box types

public class PlayerControllerTask4 : MonoBehaviour
{
    [Header("Text box references")]
    public TextMeshProUGUI ScoreTextBox;
    public TextMeshProUGUI TimerTextBox;
    public TextMeshProUGUI CompletionTextBox;

    [Header("Gameplay variables")]
    [SerializeField] int Score = 0;
    [SerializeField] float Timer = 60;
    [SerializeField] bool CompletionStatus = false;

    void Start()
    {
    }

    void Update()
    {
        Timer = Timer - Time.deltaTime;
        ScoreTextBox.text = "Your score: " + Score.ToString();
        TimerTextBox.text = "Your time: " + Timer.ToString();
        CompletionTextBox.text = "Level complete? " + CompletionStatus.ToString();
    }
}

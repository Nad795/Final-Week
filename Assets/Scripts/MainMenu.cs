using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private PlayerMovement player;

    public Sprite normalButton;
    public Sprite selectedButton;

    private int currentSelection = 0;
    private Button[] buttons;

    void Start()
    {
        buttons = new Button[] { playButton, exitButton };
        currentSelection = 0;
        EventSystem.current.SetSelectedGameObject(buttons[currentSelection].gameObject);
        player.canMove = false;
    }

    void Update()
    {
        foreach (var button in buttons)
        {
            var image = button.GetComponent<Image>();
            image.sprite = (EventSystem.current.currentSelectedGameObject == button.gameObject) ? selectedButton : normalButton;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelection = (currentSelection + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentSelection].gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSelection = (currentSelection - 1 + buttons.Length) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentSelection].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (EventSystem.current.currentSelectedGameObject == playButton.gameObject)
            {
                menuPanel.SetActive(false);
                player.canMove = true;
            }
            else if (EventSystem.current.currentSelectedGameObject == exitButton.gameObject)
            {
                Application.Quit();
            }
        }
    }
}
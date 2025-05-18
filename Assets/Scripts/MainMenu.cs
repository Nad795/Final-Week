using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject menuPanel;

    public Sprite normalButton;
    public Sprite selectedButton;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
    }

    void Update()
    {
        var playImage = playButton.GetComponent<Image>();
        var exitImage = exitButton.GetComponent<Image>();

        GameObject selected = EventSystem.current.currentSelectedGameObject;
        playImage.sprite = selected == playButton.gameObject ? selectedButton : normalButton;
        exitImage.sprite = selected == exitButton.gameObject ? selectedButton : normalButton;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selected == playButton.gameObject)
            {
                menuPanel.SetActive(false);
            }
            else if (selected == exitButton.gameObject)
            {
                Application.Quit();
            }
        }
    }
}

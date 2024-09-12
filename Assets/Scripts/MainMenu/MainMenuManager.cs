using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Image gameTitle;

    [Header("MenuButtons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button exitButton;

    [Header("MenuPanels")]
    [SerializeField] private Transform levelPanel;
    [SerializeField] private Transform optionPanel;
    [SerializeField] private Transform creditPanel;
    [SerializeField] private Transform exitPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        ButtonInitialize();
    }

    private void ButtonInitialize()
    {
        playButton.onClick.AddListener(GoToLevelMenu);
        optionButton.onClick.AddListener(GoToOption);
        creditButton.onClick.AddListener(GoToCredit);
        exitButton.onClick.AddListener(GoToExit);
    }

    private void GoToLevelMenu()
    {
        levelPanel.gameObject.SetActive(true);
        EnableMainMenu(false);    
    }

    private void GoToOption()
    {
        optionPanel.gameObject.SetActive(true);
        EnableMainMenu(false);
    }

    private void GoToCredit()
    {
        creditPanel.gameObject.SetActive(true);
        EnableMainMenu(false);
    }

    private void GoToExit()
    {
        exitPanel.gameObject.SetActive(true);
        EnableMainMenu(false);
    }

    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void EnableMainMenu(bool enabled)
    {
        mainMenu.SetActive(enabled);
    }

    public void DisablePanel(GameObject currentPanel)
    {
        currentPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

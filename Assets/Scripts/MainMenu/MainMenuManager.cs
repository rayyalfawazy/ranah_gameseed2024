using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
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
        TitleAnimation();
       // PlayButtonAnimation();

        ButtonInitialize();
    }

    private void TitleAnimation()
    {
        gameTitle.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutSine);
        gameTitle.transform.DOMoveY(gameTitle.transform.position.y + 7f, 1.5f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void PlayButtonAnimation()
    {
        playButton.transform.DOScale(new Vector2(1.15f,1.15f), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutCubic);
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

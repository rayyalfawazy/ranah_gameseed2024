using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
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

    [Header("VolumeSlider")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        ButtonInitialize();
        VolumeInitialize();
    }

    private void ButtonInitialize()
    {
        playButton.onClick.AddListener(GoToLevelMenu);
        optionButton.onClick.AddListener(GoToOption);
        creditButton.onClick.AddListener(GoToCredit);
        exitButton.onClick.AddListener(GoToExit);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void VolumeInitialize()
    {
        float bgmPrefs = PlayerPrefs.GetFloat("BGM_Volume");
        float sfxPrefs = PlayerPrefs.GetFloat("SFX_Volume");
        if (bgmPrefs == 0)
        {
            bgmSlider.value = 1;
            sfxSlider.value = 1;
        } else
        {
            bgmSlider.value = bgmPrefs;
            sfxSlider.value = sfxPrefs;
        }
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

    private void SetBGMVolume(float value)
    {
        float BGMVolume = Mathf.Log10(value) * 20;
        mixer.SetFloat("BGM", BGMVolume);

        if (BGMVolume == -40)
        {
            mixer.SetFloat("BGM", -80f);
        }

        PlayerPrefs.SetFloat("BGM_Volume",value);
    }

    private void SetSFXVolume(float value)
    {
        float SFXVolume = Mathf.Log10(value) * 20;
        mixer.SetFloat("SFX", SFXVolume);

        if (SFXVolume == -40)
        {
            mixer.SetFloat("SFX", -80f);
        }

        PlayerPrefs.SetFloat("SFX_Volume", value);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPaused = false;

    [Header("UI")]
    [SerializeField] private Transform pausePanel;

    private void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Memberhentikan waktu
        isPaused = true;
        GetComponent<GameplayManager>().player.CanShoot = false;
        GetComponent<GameplayManager>().player.transform.GetChild(0).GetComponent<PlayerHead>().enabled = false;
        pausePanel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Melanjutkan waktu
        isPaused = false;
        GetComponent<GameplayManager>().player.CanShoot = true;
        GetComponent<GameplayManager>().player.transform.GetChild(0).GetComponent<PlayerHead>().enabled = true;
        pausePanel.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}


using RRR;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    private PauseManager pauseManager;

    [Header("Gameplay")]
    public RRR_TutorialManager tutorialManager;
    public Player player;
    public bool isPlayerDead;

    [Header("UI")]
    [SerializeField] private RectTransform deadPanel;
    [SerializeField] private RectTransform winPanel;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Button restartButton;
    [SerializeField] private TMP_Text barrackCounter;

    public List<EnemyBarrack> barracks;

    private int totalBarracks;
    private int currentBarracks;

    private void Start()
    {
        pauseManager = GetComponent<PauseManager>();

        player.onPlayerDestroyed.AddListener(HandleDeath);
        player.onPlayerDamaged.AddListener(HandleDamage);
        restartButton.onClick.AddListener(RestartLevel);
        BarrackInitiation();

        healthBar.value = player.playerHP;
        totalBarracks = barracks.Count;
        currentBarracks = totalBarracks;

        SetBarrackCounterText(totalBarracks, currentBarracks);
        Tutorial();
    }

    private void HandleDeath()
    {
        pauseManager.isPaused = true;
        isPlayerDead = true;
        deadPanel.gameObject.SetActive(true);
        StopAllCoroutines();
    }

    private void Tutorial()
    {
        if (tutorialManager != null)
        {
            tutorialManager.StartTutorial();
            player.CanShoot = false;
        }
    }

    private void HandleDamage()
    {
        StartCoroutine(AnimateHealthBar(player.playerHP));
    }

    private IEnumerator AnimateHealthBar(float targetValue)
    {
        float initialValue = healthBar.value;
        float duration = 0.25f; // durasi animasi dalam detik
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            healthBar.value = Mathf.Lerp(initialValue, targetValue, elapsed / duration);
            yield return null;
        }

        healthBar.value = targetValue; // pastikan nilai akhir tepat sama dengan target
    }

    private void BarrackInitiation()
    {
        GameObject[] barrackObjects = GameObject.FindGameObjectsWithTag("EnemyBarrack");

        barracks = new List<EnemyBarrack>();

        foreach (GameObject obj in barrackObjects)
        {
            EnemyBarrack barrack = obj.GetComponent<EnemyBarrack>();
            if (barrack != null)
            {
                barracks.Add(barrack);
            }
        }
    }

    public void RemoveBarrack(EnemyBarrack barrack)
    {
        if (barracks.Contains(barrack))
        {
            barracks.Remove(barrack);

            if (barracks.Count == 0)
            {
                HandleWin();
            }

            currentBarracks = barracks.Count;
            SetBarrackCounterText(totalBarracks, currentBarracks);
        }
    }

    private void SetBarrackCounterText(int total, int current)
    {
        barrackCounter.text = $"Barracks: {current}/{total}";
    }

    private void HandleWin()
    {
        pauseManager.isPaused = true;
        winPanel.gameObject.SetActive(true);
        player.transform.GetChild(0).GetComponent<PlayerHead>().enabled = false;
        player.CanShoot = false;
    }

    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

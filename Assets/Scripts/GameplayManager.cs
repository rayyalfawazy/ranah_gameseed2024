using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [Header("Gameplay")]
    public Player player;
    public bool isPlayerDead;

    [Header("UI")]
    [SerializeField] private RectTransform deadPanel;
    [SerializeField] private RectTransform winPanel;
    [SerializeField] private Slider healthBar;

    public List<EnemyBarrack> barracks;

    private void Awake()
    {
        player.onPlayerDestroyed.AddListener(HandleDeath);
        player.onPlayerDamaged.AddListener(HandleDamage);
    }

    private void Start()
    {
        healthBar.value = player.playerHP;
        BarrackInitiation();
    }

    private void HandleDeath()
    {
        isPlayerDead = true;
        deadPanel.gameObject.SetActive(true);
        StopAllCoroutines();
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
        }
    }

    private void HandleWin()
    {
        winPanel.gameObject.SetActive(true);

        // Lakukan aksi lain jika diperlukan, misalnya berhenti game atau memutar animasi
    }
}

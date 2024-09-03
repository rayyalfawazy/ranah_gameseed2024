using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBarrack : MonoBehaviour
{
    public float barrackHP;

    [Header("UI")]
    [SerializeField] private Slider healthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        int damage = 0;

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            damage = other.GetComponent <PlayerBullet>().bulletDamage;
            Destroy(other.gameObject);
        }

        HandleBarrackDamage(damage);
        StartCoroutine(AnimateHealthBar(barrackHP));
    }

    private void OnDestroy()
    {
        StopCoroutine(AnimateHealthBar(barrackHP));
        FindObjectOfType<GameplayManager>().RemoveBarrack(this);
    }

    private void HandleBarrackDamage(int damage)
    {
        if (damage > 0)
        {
            // Logika Player Diserang
            barrackHP -= damage;
            // onPlayerDamaged.Invoke();

            if (barrackHP <= 0)
            {
                // Logika Player Mati
                Destroy(gameObject);
            }
        }
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
}

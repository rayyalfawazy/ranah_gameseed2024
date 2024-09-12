using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBarrack : MonoBehaviour
{
    public float barrackHP;

    [Header("UI")]
    [SerializeField] private Slider healthBar;

    [Header("VFX")]
    [SerializeField] private ParticleSystem fireParticle;

    private Coroutine hideHealthBarCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        int damage = 0;

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            healthBar.gameObject.SetActive(true);

            // Reset timer if coroutine is already running
            if (hideHealthBarCoroutine != null)
            {
                StopCoroutine(hideHealthBarCoroutine);
            }

            hideHealthBarCoroutine = StartCoroutine(HideHealthBarAfterDelay(2f));

            damage = other.GetComponent<PlayerBullet>().bulletDamage;
            Destroy(other.gameObject);
        }

        HandleBarrackDamage(damage);
        StartCoroutine(AnimateHealthBar(barrackHP));
    }

    private void OnDestroy()
    {
        if (hideHealthBarCoroutine != null && FindObjectOfType<GameplayManager>() != null)
        {
            StopCoroutine(hideHealthBarCoroutine);
            FindObjectOfType<GameplayManager>().RemoveBarrack(this);
        } 
        StopCoroutine(AnimateHealthBar(barrackHP));
    }

    private void HandleBarrackDamage(int damage)
    {
        if (damage > 0)
        {
            // Logika Barrack Diserang
            barrackHP -= damage;

            if (barrackHP <= 0)
            {
                // Logika Barrack Hancur
                ParticleSystem explosion = Instantiate(fireParticle, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
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

    private IEnumerator HideHealthBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        healthBar.gameObject.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    public int enemyDestroyDamage;
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    public UnityEvent onEnemyKilled;

    [Header("Gun")]
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firingPoint;
    [SerializeField, Range(0.1f, 2f)] private float fireRate;

    private Rigidbody2D rb;
    private Collider2D col;
    private float fireTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        col.enabled = false;
        StartCoroutine(EnableColliderAfterDelay(0.75f));
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Menunggu selama 1 detik
        col.enabled = true; // Mengaktifkan collider
    }

    private void Update()
    {
        if (!target) {
            GetTarget(); // Dekati target
        } else
        {
            RotateTowardsTarget(); // Memutar Kearah dari Target
            Shoot(fireRate);
        }
    }

    private void FixedUpdate()
    {
        // Maju kedepan (Local Position)
        rb.velocity = transform.up * speed;
    }

    private void Shoot(float rate)
    {
        if (fireTimer <= 0f)
        {
            Instantiate(bulletPrefabs, firingPoint.position, firingPoint.rotation);
            fireTimer = rate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            // Logika Enemy Mati
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

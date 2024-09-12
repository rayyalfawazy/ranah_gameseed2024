using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Events")]
    [HideInInspector] public UnityEvent onPlayerDestroyed;
    [HideInInspector] public UnityEvent onPlayerDamaged;

    [Header("PlayerConfig")]
    public float speed = 5f;
    public int playerHP = 100;
    public int enemyKilled;

    [Header("Gun")]
    public GameObject bulletPrefabs;
    public Transform firingPoint;
    [Range(0.1f, 2f)] 
    public float fireRate;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip gunSound;

    private Rigidbody2D rb;
    private float movementX;
    private float movementY;
    private float fireTimer;
    private bool canShoot = true;

    public bool CanShoot { 
        get { return canShoot; } 
        set { canShoot = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        if (Input.GetMouseButton(0) && fireTimer <= 0f && canShoot)
        {
            Shoot();
            fireTimer = fireRate;
        } else
        {
            fireTimer -= Time.deltaTime; // Memberikan Efek Delay Sebelum Bisa Ditembak Kembali
        }
    }

    private void FixedUpdate()
    {
        movementX = Input.GetAxisRaw("Horizontal"); // Input Horizontal Pergerakan Player
        movementY = Input.GetAxisRaw("Vertical"); // Input Vertical Pergerakan Player

        rb.velocity = new Vector2(movementX, movementY).normalized * speed;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefabs, firingPoint.position, firingPoint.rotation);
        audioSource.clip = gunSound;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        onPlayerDestroyed.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int damage = 0;

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            damage = other.GetComponent<EnemyBullet>().bulletDamage;
            Destroy(other.gameObject);
        }

        HandlePlayerDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        int damage = 0;

        if (other.gameObject.CompareTag("Enemy"))
        {
            damage = other.gameObject.GetComponent<Enemy>().enemyDestroyDamage;
            Destroy(other.gameObject);
        }

        HandlePlayerDamage(damage);
    }

    private void HandlePlayerDamage(int damage)
    {
        if (damage > 0)
        {
            // Logika Player Diserang
            playerHP -= damage;
            onPlayerDamaged.Invoke();

            if (playerHP <= 0)
            {
                // Logika Player Mati
                Destroy(gameObject);
            }
        }
    }
}
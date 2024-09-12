using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float speed = 10f;
    [SerializeField, Range(1,10)] private float lifeTime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // Hancurkan Peluru Setelah Waktu Yang Ditentukan
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed; // Peluru Menembak Kearah Atas (Local Position)
    }
}

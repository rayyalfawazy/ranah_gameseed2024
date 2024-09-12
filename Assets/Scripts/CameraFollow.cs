using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -10); // Jarak antara kamera dan player
    public float smoothTime = 0.3f; // Waktu yang dibutuhkan untuk smoothing

    private Vector3 velocity = Vector3.zero; // Kecepatan kamera, digunakan oleh SmoothDamp

    private void Update()
    {
        // Tentukan posisi target kamera (posisi player + offset)
        Vector3 targetPosition = transform.position + offset;

        // Gunakan SmoothDamp untuk secara halus menggerakkan kamera ke targetPosition
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref velocity, smoothTime);
    }
}


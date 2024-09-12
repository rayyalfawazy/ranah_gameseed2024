using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    private Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Input Posisi Mouse Sebagai Arah Player

        //Mengambil Sudut arah pointer mouse
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        // Melakukan rotasi sesuai arah mouse
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new(0, 0, -10);

    private void LateUpdate()
    {
        Camera.main.transform.position = transform.position + offset;
    }
}

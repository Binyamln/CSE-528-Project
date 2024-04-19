using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform BetaCat;
    public float smoothSpeed = 0.125f;
    public Vector3 offset; 

    void FixedUpdate()
    {
        Vector3 desiredPosition = BetaCat.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Keep the camera's original z-axis value constant
        smoothedPosition.z = transform.position.z; // Ensure the Z position is not changed.
        transform.position = smoothedPosition;
    }
}

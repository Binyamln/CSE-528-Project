using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour
{
    public Transform player;  // Reference to the player transform
    public float distanceFromPlayer = 1.0f;  // Fixed distance from the player

    private Quaternion targetRotation;
    private bool isSwinging = false;

    void Update()
    {
        if (!isSwinging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the z-position is zero to keep the sword on the same plane as the player
            
            Vector3 direction = (mousePosition - player.position).normalized; // Get normalized direction from player to mouse
            transform.position = player.position + direction * distanceFromPlayer; // Set position 1 unit away in the direction of the mouse
            transform.up = direction; // Orient the sword to point towards the mouse
        }

        if (Input.GetMouseButtonDown(0) && !isSwinging)  // Check for left mouse button click to initiate swing
        {
            StartCoroutine(SwingSword());
        }
    }

    IEnumerator SwingSword()
    {
        isSwinging = true;

        float swingAngle = 75f;  // Amount of degrees to swing
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, swingAngle);
        float duration = 0.5f;  // Duration of the swing
        float elapsed = 0;

        // Rotate the sword by 45 degrees
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Return the sword to follow the mouse after swing
        isSwinging = false;
        transform.rotation = startRotation;
    }
}

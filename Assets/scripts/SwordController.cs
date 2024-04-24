using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour
{
    public float swingDuration = 0.3f;  // Time it takes to complete the swing animation
    public float visibleDelay = 0.5f;   // Time the sword remains visible after swinging

    private bool isSwinging = false;

    void Start()
    {
        gameObject.SetActive(false);  // Start with the sword invisible
    }
void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Debug.Log("Mouse Click Detected"); // Check if this logs when clicking
        if (!isSwinging)
        {
            StartCoroutine(SwingSword());
        }
    }
}


IEnumerator SwingSword()
{
    isSwinging = true;
    Debug.Log("Starting Swing");
    gameObject.SetActive(true); // Make the sword visible

    Quaternion startRotation = transform.rotation; // Store the starting rotation
    Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180); // Target rotation after swing

    float elapsed = 0;

    while (elapsed < swingDuration)
    {
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / swingDuration);
        elapsed += Time.deltaTime;
        yield return null;
    }

    transform.rotation = endRotation; // Ensure the sword reaches the final rotation
    yield return new WaitForSeconds(visibleDelay); // Keep the sword visible for a short delay

    gameObject.SetActive(false); // Make the sword invisible again
    transform.rotation = startRotation; // Reset rotation for next swing
    isSwinging = false;
    Debug.Log("Ending Swing");
}
}

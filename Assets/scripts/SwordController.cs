using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour
{
    public float swingDuration = 0.3f;  // Time it takes to complete the swing animation
    public float visibleDelay = 0.5f;   // Time the sword remains visible after swinging

    private bool isSwinging = false;
    private Renderer swordRenderer;     

    void Start()
    {
        swordRenderer = GetComponent<Renderer>(); 
        swordRenderer.enabled = false;  // Start with the sword invisible
    }

    public void InitiateSwing(Vector2 direction)
    {
        if (!isSwinging)
        {
            StartCoroutine(SwingSword(direction));
        }
    }

    IEnumerator SwingSword(Vector2 direction)
    {
        isSwinging = true;
        swordRenderer.enabled = true;  // Make the sword visible

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // Calculate the angle
        Quaternion startRotation = Quaternion.Euler(0, 0, angle);
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, -90);  // Rotate -90 degrees from the start

        transform.rotation = startRotation; 

        float elapsed = 0;
        while (elapsed < swingDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / swingDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;  
        yield return new WaitForSeconds(visibleDelay);  

        swordRenderer.enabled = false;  // Make the sword invisible again
        isSwinging = false;
    }
}

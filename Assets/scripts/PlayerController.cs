using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player's movement
    public float dashDistance = 10.0f; // Distance of the dash
    public float dashCooldown = 1.0f; // Time between dashes

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float lastDashTime = -100f; // Initialize to a large negative number

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
            lastDashTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
    }

    private void Dash()
    {
        // Calculate dash direction
        Vector2 dashDirection = moveInput.normalized; // Ensure the dash direction is normalized
        if (dashDirection != Vector2.zero) // Checks if there is a direction input
        {
            rb.MovePosition(rb.position + dashDirection * dashDistance); // Apply the dash
        }
    }
}
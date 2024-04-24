using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player's movement
    public float dashDistance = 10.0f; // Distance of the dash
    public float dashCooldown = 1.0f; // Time between dashes
    public int health = 100; // Player's health as an integer
    public int damage = 10; // Player's damage as integer

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float lastDashTime = -100f; 

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
        Vector2 dashDirection = moveInput.normalized; 
        if (dashDirection != Vector2.zero) 
        {
            rb.MovePosition(rb.position + dashDirection * dashDistance); 
        }
    }

    // Updated method to take damage using integer health
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player is dead!"); // Handle player death 
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Health") {
            health += 10;
            Debug.Log("Health: " + health);
        }
        if (collision.tag == "Speed") {
            speed += 0.2f;
            Debug.Log("Speed: " + speed);
        }
    }
}

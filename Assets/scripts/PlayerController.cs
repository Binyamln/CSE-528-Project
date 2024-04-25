using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject sword;
    private Animator animator;
    public float speed = 5.0f; // Speed of the player's movement
    public float dashDistance = 10.0f; // Distance of the dash
    public float dashCooldown = 1.0f; // Time between dashes
    public int health = 100; // Player's max health as an integer
    public int current_health = 100; // Player's current health as an integer
    public int damage = 10; // Player's damage as an integer

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float lastDashTime = -100f; 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        animator.SetFloat("moreX", moveInput.x);
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
            lastDashTime = Time.time;
        }
        if (current_health <= 0) {
            Debug.Log("The Cat Dies!!!");
            Destroy(gameObject);
            SceneManager.LoadScene("DeathScreen");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
        transform.rotation = Quaternion.Euler(0, 0, 0); // Keep rotation at zero
    }

    private void Dash()
    {
        Vector2 dashDirection = moveInput.normalized; 
        if (dashDirection != Vector2.zero) 
        {
            rb.MovePosition(rb.position + dashDirection * dashDistance); 
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player is dead!"); // Handle player death 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Health") {
            health += 10;
            current_health = health;
            Debug.Log("Health: " + health);
        }
        if (collision.tag == "Speed") {
            speed += 0.2f;
            Debug.Log("Speed: " + speed);
        }
        if (collision.tag == "Damage") {
            damage += 10;
            Debug.Log("Damage: " + damage);
        }
        if (collision.tag == "Enemy") {
            current_health -= 20;
            Debug.Log("Health: " + current_health);
        }
    }
}

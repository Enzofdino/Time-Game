using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 10f;  // Force applied when jumping
    private bool isGrounded;       // Check if the player is on the ground
    private Rigidbody2D rb;        // Reference to the Rigidbody2D component
    //static public GameManager instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("PlayerJump script started");
    }

    void Update()
    {
        // Check if the player is pressing the jump key (space bar) and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Debug.Log("Space key pressed");
            if (isGrounded == true)
            {
                isGrounded = false;
                Jumping();
            }
        }
    }

    void Jumping()
    {
        // Apply a vertical force to the Rigidbody2D to make the player jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        Debug.Log("Jump executed");
    }

    // Check for collision with the ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Player grounded");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Player not grounded");
        }
    }


}

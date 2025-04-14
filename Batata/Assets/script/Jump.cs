using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 10;  // Force applied when jumping
    private bool isGrounded;       // Check if the player is on the ground
    private Rigidbody2D rb;        // Reference to the Rigidbody2D component
    public float energyRecoveryDelay = 3f;
    public float energy = 6;
    private bool superJump = false;
    public bool CanJump = true;
    //static public GameManager instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("PlayerJump script started");
        jumpForce = 10;

    }


    void Update()
    {
        // Alterna entre pulo normal e pulo especial
        if (Input.GetKeyDown(KeyCode.J))
        {
            superJump = !superJump;
            jumpForce = superJump ? 15 : 10;
            Debug.Log("Super pulo ativado? " + superJump);
        }

        // Verifica se pode pular
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && CanJump)
        {
            int custoPulo = superJump ? 2 : 0;

            if (energy >= custoPulo)
            {
                energy -= custoPulo;
                Debug.Log("Energia restante: " + energy);
                Jumping();

                if (energy <= 0)
                {
                    CanJump = false;
                    StartCoroutine(RecuperarEnergia());
                }
            }
        }
        else
        {
            Debug.Log("Sem energia para pular!");

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

        IEnumerator RecuperarEnergia()
        {
            Debug.Log("Recuperando energia em " + energyRecoveryDelay + " segundos...");
            yield return new WaitForSeconds(energyRecoveryDelay);

            energy = 6;
            CanJump = true;
            Debug.Log("Energia restaurada!");
        }

    
}


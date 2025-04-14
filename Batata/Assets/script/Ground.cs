using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Material material;
    public float scrollSpeed = 1.0f; // Speed to scroll the texture
    private bool playerOnGround = false;
    public Coroutine groundCheckCoroutine;


    private void Start()
    {
        // Get the material from the SpriteRenderer component
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        // Offset the texture to create a scrolling effect
        material.mainTextureOffset += new Vector2(GameManager.Instance.Speed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnGround = true;
            if (groundCheckCoroutine != null)
            {
                StopCoroutine(groundCheckCoroutine);
                groundCheckCoroutine = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnGround = false;
            groundCheckCoroutine = StartCoroutine(CheckPlayerGrounded());
        }
    }


    private IEnumerator CheckPlayerGrounded()
    {
        yield return new WaitForSeconds(0f);

        if (!playerOnGround)
        {
            Debug.Log("Game Over: jogador ficou fora do chão por 3 segundos.");
            Gameover.instance.AtivarGameOver(); // Supondo que você tenha um método assim
        }
    }
}
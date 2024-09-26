using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafolow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public Sprite backgroundSprite; // Assign your background sprite in the inspector
    private GameObject backgroundObject;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }

        // Create and set up the background
        CreateBackground();
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Move the camera to follow the player
            transform.position = player.position + offset;

            // Move the background to follow the camera
            if (backgroundObject != null)
            {
                backgroundObject.transform.position = new Vector3(transform.position.x, transform.position.y, 10);
            }
        }
    }

    private void CreateBackground()
    {
        // Create a new GameObject to hold the background
        backgroundObject = new GameObject("Background");

        // Add a SpriteRenderer component and set the sprite
        spriteRenderer = backgroundObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = backgroundSprite;

        // Set the sorting order to ensure it's behind other sprites
        spriteRenderer.sortingOrder = -100;

        /*// Adjust the scale to fit the camera's view
        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float screenWidth = screenHeight * Camera.main.aspect;
        backgroundObject.transform.localScale = new Vector3(screenWidth, screenHeight, 1);*/
    }
}
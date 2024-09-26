using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Material material;
    public float scrollSpeed = 1.0f; // Speed to scroll the texture

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
}
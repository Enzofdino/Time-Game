using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafolow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public Sprite backgroundSprite;
    private GameObject backgroundObject;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }

        
        CreateBackground();
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            
            transform.position = player.position + offset;

           
            if (backgroundObject != null)
            {
                backgroundObject.transform.position = new Vector3(transform.position.x, transform.position.y, 10);
            }
        }
    }

    private void CreateBackground()
    {
       
        backgroundObject = new GameObject("Background");

       
        spriteRenderer = backgroundObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = backgroundSprite;

        
        spriteRenderer.sortingOrder = -100;
 
    }
}
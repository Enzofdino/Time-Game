using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public float speed = 10;
    private float direction;
    private Rigidbody2D rb;         // Reference to the Rigidbody2D component
    public Transform groundCheck;   // Reference to the GroundCheck GameObject
    public LayerMask groundLayer;   // Layer mask to specify what counts as ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        Move();
        //FlipSprite();

    }

    void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    //void FlipSprite()
    //{
    //    if (direction < 0)
    //    {
    //        transform.localScale = new Vector3(-0.4977f, 0.4977f, 0.4977f);
    //    }
    //    else if (direction > 0)
    //    {
    //        transform.localScale = new Vector3(0.4977f, 0.4977f, 0.4977f);
    //    }
    //}   
}

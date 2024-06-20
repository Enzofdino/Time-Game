using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    Vector2 screenBounds;
    float speed = 4;

    public Vector2 ScreenBounds { get => screenBounds; }

    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        Instance = this;    
        screenBounds = new Vector3(-1, 1) + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    public static implicit operator GameManager(player v)
    {
        throw new NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafolow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }
    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Material texture;
    private void Update()
    {
        texture.mainTextureOffset += new Vector2(GameManager.Instance.Speed * Time.deltaTime, 0);
    }
}

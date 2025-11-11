using UnityEngine;

public class MirrorRotator : MonoBehaviour
{
    public float anguloRotacao = 90f;

    public void Girar()
    {
        transform.Rotate(0, 0, anguloRotacao);
    }
}

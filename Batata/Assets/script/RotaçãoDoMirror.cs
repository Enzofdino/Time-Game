using UnityEngine;

public class RotacaoDoMirror : MonoBehaviour
{
    public float anguloRotacao = 10f;
    private bool jogadorDentro = false;

    void Update()
    {
        // Só verifica a tecla se o player estiver dentro
        if (jogadorDentro && Input.GetKeyDown(KeyCode.T))
        {
            transform.Rotate(0, 0, anguloRotacao);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDentro = false;
        }
    }
}

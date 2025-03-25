using System.Collections;
using UnityEngine;

public class PlataformaVoadora : MonoBehaviour
{
    [SerializeField] private float tempoVisivel = 2f; // Tempo visível
    [SerializeField] private float tempoInvisivel = 2f; // Tempo invisível
    private SpriteRenderer spriteRenderer;
    public Collider2D colisor; // Alterado para Collider2D

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisor = GetComponent<Collider2D>(); // Garante que o Collider2D seja corretamente atribuído

        if (colisor == null)
        {
            Debug.LogError("Nenhum Collider2D encontrado na plataforma!", this);
        }

        StartCoroutine(ControlarPlataforma());
    }

    private IEnumerator ControlarPlataforma()
    {
        while (true) // Loop infinito para repetir o ciclo
        {
            while (Contador.isTimeFrozen) // Se o tempo estiver pausado, a plataforma mantém o estado atual
            {
                yield return null;
            }

            // Plataforma visível
            spriteRenderer.enabled = true;
            colisor.enabled = true;
            yield return new WaitForSeconds(tempoVisivel);

            while (Contador.isTimeFrozen) // Aguarda enquanto o tempo estiver pausado
            {
                yield return null;
            }

            // Plataforma invisível
            spriteRenderer.enabled = false;
            colisor.enabled = false;
            yield return new WaitForSeconds(tempoInvisivel);
        }
    }
}

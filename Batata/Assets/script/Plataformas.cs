using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps; // Importa suporte para Tilemaps

public class PlataformaVoadora : MonoBehaviour
{
    static public PlataformaVoadora instance;
    void Awake()
    {
        instance = this;
    }
    [SerializeField] private float tempoVisivel = 2f;
    [SerializeField] private float tempoInvisivel = 2f;

    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public TilemapCollider2D tilemapCollider;

    void Start()
    {
        // Obtém os componentes corretos do Tilemap
        tilemap = GetComponent<Tilemap>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();

        // Verifica se os componentes foram encontrados
        if (tilemap == null || tilemapRenderer == null || tilemapCollider == null)
        {
            Debug.LogError("PlataformaVoadora: Faltam componentes Tilemap!", this);
            return;
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
            tilemapRenderer.enabled = true;
            tilemapCollider.enabled = true;
            yield return new WaitForSeconds(tempoVisivel);

            while (Contador.isTimeFrozen) // Aguarda enquanto o tempo estiver pausado
            {
                yield return null;
            }

            // Plataforma invisível
            tilemapRenderer.enabled = false;
            tilemapCollider.enabled = false;
            yield return new WaitForSeconds(tempoInvisivel);
        }
    }
}


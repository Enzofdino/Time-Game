using UnityEngine;
using UnityEngine.Tilemaps;

public class CavernaTransparente : MonoBehaviour
{
    [SerializeField] private TilemapRenderer tilemapRenderer;
    [SerializeField] private float transparencia = 0.3f;

    Color corOriginal;

    void Start()
    {
        if (tilemapRenderer == null)
            tilemapRenderer = GetComponent<TilemapRenderer>();

        corOriginal = tilemapRenderer.material.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Color cor = tilemapRenderer.material.color;
            cor.a = transparencia;
            tilemapRenderer.material.color = cor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tilemapRenderer.material.color = corOriginal;
        }
    }


}

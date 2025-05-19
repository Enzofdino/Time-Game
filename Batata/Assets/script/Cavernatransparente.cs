using UnityEngine;
using UnityEngine.Tilemaps;

public class CavernaTransparente : MonoBehaviour
{
    [SerializeField] private TilemapRenderer tilemapRenderer;
    [SerializeField] private float transparencia = 0.3f;
    [SerializeField] private SpriteRenderer portao;

    private Color corOriginal;

    void Start()
    {
        if (tilemapRenderer == null)
            tilemapRenderer = GetComponent<TilemapRenderer>();

   
        corOriginal = tilemapRenderer.material.color;

      
        if (portao != null)
            portao.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          
            Color cor = tilemapRenderer.material.color;
            cor.a = transparencia;
            tilemapRenderer.material.color = cor;

    
            if (portao != null)
                portao.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            tilemapRenderer.material.color = corOriginal;

            
            if (portao != null)
                portao.enabled = false;
        }
    }
}

using System.Collections;
using UnityEngine;

public class Espinhos : MonoBehaviour
{
    public static Espinhos instance;

    public SpriteRenderer spriteRenderer;
    public Collider2D spikeCollider;
   

    private bool ativo = true; // Controla se os espinhos estão visíveis

    void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spikeCollider = GetComponent<Collider2D>();
       
    }

    void Start()
    {
        StartCoroutine(AlternarEspinhos());
    }

    IEnumerator AlternarEspinhos()
    {
        while (true)
        {
            // Aguarda enquanto o tempo estiver congelado
            while (Contador.isTimeFrozen)
            {
                yield return null;
            }

            // Espinhos aparecem
            spriteRenderer.enabled = true;
            spikeCollider.enabled = true;
          
            ativo = true;
            yield return new WaitForSeconds(1f);

            // Aguarda enquanto o tempo estiver congelado
            while (Contador.isTimeFrozen)
            {
                yield return null;
            }

            // Espinhos somem
            spriteRenderer.enabled = false;
            spikeCollider.enabled = false;
            
            ativo = false;
            yield return new WaitForSeconds(2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jogador colidiu com espinhos!");
            Gameover.instance.AtivarGameOver();
        }
    }


}

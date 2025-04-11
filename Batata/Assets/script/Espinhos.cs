using System.Collections;
using UnityEngine;

public class Espinhos : MonoBehaviour
{
    public static Espinhos instance;

    public SpriteRenderer spriteRenderer;
    public Collider2D spikeCollider;

    [SerializeField] private float tempoAlternancia = 1f;
    [SerializeField] private float tempoAlternancia1 = 1f;
    private bool ativo = true;

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
            while (Contador.isTimeFrozen)
            {
                yield return null;
            }

            spriteRenderer.enabled = true;
            spikeCollider.enabled = true;
            ativo = true;
            yield return new WaitForSeconds(tempoAlternancia);

            while (Contador.isTimeFrozen)
            {
                yield return null;
            }

            spriteRenderer.enabled = false;
            spikeCollider.enabled = false;
            ativo = false;
            yield return new WaitForSeconds(tempoAlternancia1);
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

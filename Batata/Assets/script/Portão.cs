using System.Collections;
using UnityEngine;

public class CodigoSecreto : MonoBehaviour
{
  static public  CodigoSecreto instance;
    void Awake()
    {
        instance = this;
    }
    public GameObject portao;
    public GameObject[] interruptores; // Array de interruptores
    public int[] ordemCorreta;         // Sequência correta
    public float tempoMaximo = 10f;

    private int indiceAtual = 0;
    private float tempoRestante;
    public bool resolvido = false;

    private GameObject interruptorAtual;

    void Start()
    {
        tempoRestante = tempoMaximo;
        AtualizarPortao(false);
    }

    void Update()
    {
        if (resolvido) return;

        if (!Contador.isTimeFrozen)
        {
            tempoRestante -= Time.deltaTime;
        }

        if (tempoRestante <= 0)
        {
            GameOver();
        }

        // Apertar "E" para acionar
        if (interruptorAtual != null && Input.GetKeyDown(KeyCode.E))
        {
            Interruptor interruptor = interruptorAtual.GetComponent<Interruptor>();
            if (interruptor != null)
            {
                AcionarInterruptor(interruptor.id);
            }
        }
    }

    public void AcionarInterruptor(int id)
    {
        if (resolvido) return;

        if (id == ordemCorreta[indiceAtual])
        {
            Debug.Log("Interruptor correto: " + id);
            indiceAtual++;

            if (indiceAtual >= ordemCorreta.Length)
            {
                PuzzleResolvido();
            }
        }
        else
        {
            Debug.Log("Erro! Sequência incorreta.");
            GameOver();
        }
    }

  public  void PuzzleResolvido()
    {
        resolvido = true;
        AtualizarPortao(true);
        Debug.Log("Portão destravado!");
    }

    void GameOver()
    {
        resolvido = false;
        indiceAtual = 0;
        tempoRestante = tempoMaximo;
        Debug.Log("Tempo esgotado ou sequência errada! Reiniciando...");
    }

    void AtualizarPortao(bool abrir)
    {
        if (portao != null)
        {
            portao.SetActive(!abrir); // Se abrir for true, portão some
        }
    }

    public void ResetarPuzzle()
    {
        indiceAtual = 0;
        tempoRestante = tempoMaximo;
        resolvido = false;
        AtualizarPortao(false);
        Debug.Log("Puzzle reiniciado.");
    }

    public float TempoRestante()
    {
        return tempoRestante;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interruptor"))

        {
            Debug.Log("Entrou em contato com: " + other.name);
            interruptorAtual = other.gameObject;
            Debug.Log("Aperte 'E' para acionar o interruptor.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interruptor") && other.gameObject == interruptorAtual)
        {
            interruptorAtual = null;
        }
    }
}

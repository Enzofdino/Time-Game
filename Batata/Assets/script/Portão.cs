using System.Collections;
using UnityEngine;

public class CodigoSecreto : MonoBehaviour
{
    public GameObject portao;
    public GameObject[] interruptores; // Array de interruptores
    public int[] ordemCorreta;         // Sequência correta
    public float tempoMaximo = 10f;    // Tempo para resolver o puzzle

    private int indiceAtual = 0;       // Índice da sequência atual
    private float tempoRestante;       // Tempo restante
    private bool resolvido = false;    // Controle de resolução
    private GameObject interruptorAtual; // Interruptor próximo

    void Start()
    {
        tempoRestante = tempoMaximo;
        AtualizarPortao(false); // Portão inicialmente fechado
    }

    void Update()
    {
        if (!resolvido)
        {
            // Atualiza o tempo restante
            if (!Contador.isTimeFrozen)
            {
                tempoRestante -= Time.deltaTime;
            }

            // Game Over se o tempo acabar
            if (tempoRestante <= 0)
            {
                GameOver();
            }

            // Verifica se o jogador pressiona "E" para acionar o interruptor
            if (interruptorAtual != null && Input.GetKeyDown(KeyCode.E))
            {
                int id = interruptorAtual.GetComponent<Interruptor>().id;
                AcionarInterruptor(id);
            }
        }
    }

    public void AcionarInterruptor(int id)
    {
        if (resolvido) return;

        // Verifica se o interruptor acionado está na sequência correta
        if (id == ordemCorreta[indiceAtual])
        {
            Debug.Log("Interruptor correto: " + id);
            indiceAtual++;

            // Verifica se completou a sequência
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

    void PuzzleResolvido()
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
        portao.SetActive(!abrir); // Se abrir for true, o portão some
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

    // Verifica se o jogador está próximo de um interruptor
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interruptor"))
        {
            interruptorAtual = other.gameObject;
            Debug.Log("Aperte 'E' para acionar o interruptor.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interruptor"))
        {
            if (other.gameObject == interruptorAtual)
            {
                interruptorAtual = null;
            }
        }
    }
}

using System.Collections;
using UnityEngine;

public class CodigoSecreto : MonoBehaviour
{
    public static CodigoSecreto instance;

    void Awake()
    {
        instance = this;
    }

    [Header("Puzzle dos Interruptores")]
    public GameObject[] interruptores;
    public int[] ordemCorreta;
    public float tempoMaximo = 10f;

    private int indiceAtual = 0;
    private float tempoRestante;
    private GameObject interruptorAtual;

    [Header("Portão Final")]
    public GameObject portao;
    public SpriteRenderer portaoRenderer;
    public Collider2D portaoCollider;
    public GameObject mensagemAbrirPortaoUI;

    private bool jogadorPertoDoPortao = false;
    private GameObject jogador;

    [Header("Chave")]
    public GameObject chavePrefab; // <- arraste o prefab da chave aqui no inspetor
    private GameObject chaveInstanciada;
    public bool temChave = false;

    public bool resolvido = false;

    void Start()
    {
        tempoRestante = tempoMaximo;
        AtualizarPortao(false);

        if (mensagemAbrirPortaoUI != null)
            mensagemAbrirPortaoUI.SetActive(false);

        // Spawn da chave no início do jogo
        if (chavePrefab != null)
        {
            chaveInstanciada = Instantiate(chavePrefab, new Vector3(60.36f, 17.54f, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        if (resolvido && jogadorPertoDoPortao && temChave && Input.GetKeyDown(KeyCode.E))
        {
            AbrirPortaoFinal();
            return;
        }

        if (resolvido) return;

        if (!Contador.isTimeFrozen)
        {
            tempoRestante -= Time.deltaTime;
        }

        if (tempoRestante <= 0)
        {
            GameOver();
        }

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

    public void PuzzleResolvido()
    {
        resolvido = true;
        AtualizarPortao(false); // Portão ainda fechado, só será aberto com a chave
        Debug.Log("Puzzle resolvido! Agora pegue a chave e vá até o portão.");
    }

    void GameOver()
    {
        resolvido = false;
        indiceAtual = 0;
        tempoRestante = tempoMaximo;
        Debug.Log("Tempo esgotado. Puzzle reiniciado.");
    }

    void AtualizarPortao(bool abrir)
    {
        if (portaoRenderer != null)
            portaoRenderer.enabled = !abrir;

        if (portaoCollider != null)
            portaoCollider.isTrigger = abrir;
    }

    void AbrirPortaoFinal()
    {
        Debug.Log("Portão final aberto com a chave!");
        AtualizarPortao(true);

        if (mensagemAbrirPortaoUI != null)
            mensagemAbrirPortaoUI.SetActive(false);
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
            interruptorAtual = other.gameObject;
            Debug.Log("Aperte 'E' para acionar o interruptor.");
        }

        if (other.CompareTag("Player"))
        {
            if (resolvido && temChave)
            {
                jogadorPertoDoPortao = true;
                jogador = other.gameObject;

                if (mensagemAbrirPortaoUI != null)
                    mensagemAbrirPortaoUI.SetActive(true);
            }
        }

        if (other.CompareTag("Chave"))
        {
            temChave = true;
            Destroy(other.gameObject);
            Debug.Log("Chave coletada!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interruptor") && other.gameObject == interruptorAtual)
        {
            interruptorAtual = null;
        }

        if (other.CompareTag("Player"))
        {
            jogadorPertoDoPortao = false;

            if (mensagemAbrirPortaoUI != null)
                mensagemAbrirPortaoUI.SetActive(false);
        }
    }
}

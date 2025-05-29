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
    public float tempoMaximo = 15f;

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

    [Header("Chaves")]
    public GameObject chave1Prefab;
    public GameObject chave2Prefab;

    private GameObject chave1Instanciada;
    private GameObject chave2Instanciada;

    public bool temChave1 = false;
    public bool temChave2 = false;


    public bool resolvido = false;

    void Start()
    {
        tempoRestante = tempoMaximo;
        AtualizarPortao(false);

        if (mensagemAbrirPortaoUI != null)
            mensagemAbrirPortaoUI.SetActive(false);

        if (chave1Prefab != null)
        {
            chave1Instanciada = Instantiate(chave1Prefab, new Vector3(-27.64f, -17.49f, 0), Quaternion.identity);
        }

        if (chave2Prefab != null)
        {
            chave2Instanciada = Instantiate(chave2Prefab, new Vector3(60.39f, 17.49f, 0), Quaternion.identity);
        }


    }

    void Update()
    {
        if (resolvido)
        {
            VerificarInteracaoPortao();
        }

        // ⏳ Contador de tempo (apenas se não estiver resolvido nem congelado)
        if (!resolvido && !Contador.isTimeFrozen)
        {
            tempoRestante -= Time.deltaTime;

            if (tempoRestante <= 0)
            {
                GameOver();
            }
        }

        // 🎮 Interação com interruptores (permitida mesmo com tempo congelado)
        if (!resolvido && interruptorAtual != null && Input.GetKeyDown(KeyCode.E))
        {
            Interruptor interruptor = interruptorAtual.GetComponent<Interruptor>();
            if (interruptor != null)
            {
                AcionarInterruptor(interruptor.id);
            }
        }
    }


    void VerificarInteracaoPortao()
    {
        if (resolvido && temChave1 && temChave2 && Input.GetKeyDown(KeyCode.E))
        {
            AbrirPortaoFinal();
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
        AtualizarPortao(false);
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
        bool mostrar = !abrir;

        // Garante que o renderer principal seja escondido
        if (portao != null)
        {
            SpriteRenderer mainRenderer = portao.GetComponent<SpriteRenderer>();
            if (mainRenderer != null)
                mainRenderer.enabled = mostrar;
        }

        // Também esconde o colisor principal
        if (portaoCollider != null)
            portaoCollider.enabled = mostrar;

        // Filhos
        foreach (Transform child in portao.transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = mostrar;

            Collider2D col = child.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = mostrar;
                col.isTrigger = abrir;
            }
        }
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
        else if (other.CompareTag("PortaoTrigger"))
        {
            jogadorPertoDoPortao = true;

            if (resolvido && temChave1 && temChave2)

            {
                mensagemAbrirPortaoUI.SetActive(true);
                Debug.Log("Aperte 'E' para abrir o portão.");
            }
        }
        else if (other.CompareTag("Chave1"))
        {
            temChave1 = true;
            Destroy(other.gameObject);
            Debug.Log("Chave 1 coletada!");
        }
        else if (other.CompareTag("Chave2"))
        {
            temChave2 = true;
            Destroy(other.gameObject);
            Debug.Log("Chave 2 coletada!");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interruptor") && other.gameObject == interruptorAtual)
        {
            interruptorAtual = null;
        }

        if (other.CompareTag("PortaoTrigger"))
        {
            jogadorPertoDoPortao = false;

            if (mensagemAbrirPortaoUI != null)
                mensagemAbrirPortaoUI.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tochas : MonoBehaviour
{
    [Header("Prefabs de Tochas Coloridas")]
    [SerializeField] GameObject tochaVermelhaPrefab;
    [SerializeField] GameObject tochaAzulPrefab;
    [SerializeField] GameObject tochaLaranjaPrefab;
    [SerializeField] GameObject tochaAmarelaPrefab;
    [SerializeField] GameObject itemUI;

    float minX = -24.176f, maxX = -31.86f;
    float minY = -10.297f, maxY = -10.297f;

    List<Vector3> posicoesUsadas = new List<Vector3>();
    List<TochaClickavel> tochas = new List<TochaClickavel>();
    List<string> ordemCorreta = new List<string>();
    List<string> cliquesDoJogador = new List<string>();
   

    public bool portaoAberto = false;

    void Start()
    {
        SpawnarTochas();
        GerarOrdemCorreta();
        

    }

    void SpawnarTochas()
    {
        posicoesUsadas.Clear();
        tochas.Clear();

        Dictionary<string, GameObject> coresPrefabs = new Dictionary<string, GameObject>()
        {
            { "vermelha", tochaVermelhaPrefab },
            { "azul", tochaAzulPrefab },
            { "laranja", tochaLaranjaPrefab },
            { "amarela", tochaAmarelaPrefab }
        };

        List<string> cores = new List<string>(coresPrefabs.Keys);

        foreach (string cor in cores)
        {
            Vector3 pos;
            int tentativas = 0;

            do
            {
                pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                tentativas++;
            } while (PosicaoMuitoPerto(pos) && tentativas < 100);

            posicoesUsadas.Add(pos);

            GameObject tocha = Instantiate(coresPrefabs[cor], pos, Quaternion.identity);
            

            TochaClickavel click = tocha.AddComponent<TochaClickavel>();
            click.Definir(this, cor); // Agora passamos a *cor* como identificador

            tochas.Add(click);
        }
    }

    bool PosicaoMuitoPerto(Vector3 novaPos)
    {
        foreach (Vector3 pos in posicoesUsadas)
        {
            if (Vector3.Distance(novaPos, pos) < 1.5f)
                return true;
        }
        return false;
    }

    void GerarOrdemCorreta()
    {
        ordemCorreta = new List<string> { "vermelha", "azul", "amarela", "laranja" };
    

    Debug.Log("Ordem correta: " + string.Join(" -> ", ordemCorreta));
    }


    public void TentarClicar(string cor)
    {
        if (portaoAberto) return;

        cliquesDoJogador.Add(cor);

        int idx = cliquesDoJogador.Count - 1;
        if (ordemCorreta[idx] == cor)
        {
            if (cliquesDoJogador.Count == ordemCorreta.Count)
            {
                Debug.Log("Desafio completo! Portão pode ser aberto.");
                portaoAberto = true;
            }
        }
        else
        {
            Debug.Log("Erro! Ordem incorreta.");
            ResetarTochas();
        }
        if (ordemCorreta[idx] == cor)
        {
            Debug.Log("Cor correta clicada: " + cor);

            if (cliquesDoJogador.Count == ordemCorreta.Count)
            {
                Debug.Log("Desafio completo! Portão pode ser aberto.");
                portaoAberto = true;
            }
        }
        else
        {
            Debug.Log("Erro! Ordem incorreta. Clicou: " + cor + ", mas esperava: " + ordemCorreta[idx]);
            ResetarTochas();
        }
        if (cliquesDoJogador.Count == ordemCorreta.Count)
        {
            Debug.Log("Desafio completo! Portão pode ser aberto.");
            portaoAberto = true;

            itemUI.SetActive(true); // Ativa o item
        }

    }


    void ResetarTochas()
    {
        cliquesDoJogador.Clear();
        ordemCorreta.Clear();

        foreach (TochaClickavel tocha in tochas)
            Destroy(tocha.gameObject);

        SpawnarTochas();
        GerarOrdemCorreta();
    }
    public IEnumerator MostrarOrdem()
    {
        yield return StartCoroutine(PiscarTochasNaOrdem());
    }

    IEnumerator PiscarTochasNaOrdem()
    {
        foreach (string cor in ordemCorreta)
        {
            TochaClickavel tocha = tochas.Find(t => t.GetCor() == cor);
            if (tocha != null)
            {
                yield return StartCoroutine(tocha.Piscar());
                yield return new WaitForSeconds(0.2f);
            }
        }
    }







}

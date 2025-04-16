using System.Collections.Generic;
using UnityEngine;

public class Tochas : MonoBehaviour
{
    public static Tochas instance;

    [SerializeField]
    public GameObject tochaAcessa;
    [SerializeField]
    public Sprite tochaApagada;
    [SerializeField]
    public Sprite tochaErrada;

    public bool temItem = false;

    private List<TochaData> tochasGeradas = new List<TochaData>();
    private List<int> ordemCorreta = new List<int>();
    private List<int> cliquesDoJogador = new List<int>();

    int quantidadeGerada;

    private void Awake()
    {
        quantidadeGerada = Random.Range(2, 5);
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < quantidadeGerada; i++)
        {
            GerarTochas(i);
        }

        // Exemplo: gera uma ordem aleatória de índices
        ordemCorreta = GerarOrdemCorreta();
    }

    void GerarTochas(int indice)
    {
        GameObject novaTocha = Instantiate(tochaAcessa, new Vector3(Random.Range(-30.91f, -39.66f), -5.51f), Quaternion.identity);
        float valorAleatorio = Random.Range(0f, 1f);
        TochaData novaTochaData = new TochaData(novaTocha, valorAleatorio, indice);

        TochasClickavel clickavel = novaTocha.AddComponent<TochasClickavel>();
        clickavel.DefinirTocha(novaTochaData);

        tochasGeradas.Add(novaTochaData);
    }

    List<int> GerarOrdemCorreta()
    {
        List<int> ordem = new List<int>();
        List<int> indicesDisponiveis = new List<int>();

        for (int i = 0; i < quantidadeGerada; i++)
            indicesDisponiveis.Add(i);

        while (ordem.Count < quantidadeGerada)
        {
            int rand = Random.Range(0, indicesDisponiveis.Count);
            ordem.Add(indicesDisponiveis[rand]);
            indicesDisponiveis.RemoveAt(rand);
        }

        Debug.Log("Ordem correta: " + string.Join(", ", ordem));
        return ordem;
    }

    public void TochaClicada(int index, SpriteRenderer spriteRenderer)
    {
        if (temItem) return;

        cliquesDoJogador.Add(index);

        // Verifica se o clique atual está correto
        if (cliquesDoJogador.Count <= ordemCorreta.Count)
        {
            if (ordemCorreta[cliquesDoJogador.Count - 1] == index)
            {
                spriteRenderer.sprite = tochaApagada;

                // Se finalizou com sucesso
                if (cliquesDoJogador.Count == ordemCorreta.Count)
                {
                    temItem = true;
                    Debug.Log("Todas tochas clicadas corretamente! Item obtido.");
                }
            }
            else
            {
                spriteRenderer.sprite = tochaErrada;
                cliquesDoJogador.Clear();
                Debug.Log("Ordem errada. Tente novamente.");
            }
        }
    }
}

[System.Serializable]
public class TochaData
{
    public GameObject tocha;
    public float valor;
    public int index;

    public TochaData(GameObject tocha, float valor, int index)
    {
        this.tocha = tocha;
        this.valor = valor;
        this.index = index;
    }
}

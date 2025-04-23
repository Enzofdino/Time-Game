using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Tochas : MonoBehaviour
{
    public static Tochas instance;

    [SerializeField]
    public GameObject tochaAcessa;
    [SerializeField]
    public Sprite tochaApagada;
    [SerializeField]
    public Sprite tochaErrada;
    [SerializeField]
    public GameObject itemPremioPrefab; // Item que será spawnado
    [SerializeField]
    public GameObject imagemDoItemUI; // Referência ao ícone no Canvas

    public bool temItem = false;

    private List<TochaData> tochasGeradas = new List<TochaData>();
    private List<int> ordemCorreta = new List<int>();
    private List<int> cliquesDoJogador = new List<int>();

    int quantidadeGerada;

    private void Awake()
    {
        quantidadeGerada = 5; // Agora gera corretamente 5
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < quantidadeGerada; i++)
        {
            GerarTochas(i);
        }

        ordemCorreta = GerarOrdemCorreta();
        StartCoroutine(PiscarTochasNaOrdem());
    }

    void GerarTochas(int indice)
    {
        Vector3 pos = new Vector3(Random.Range(-39.66f, -30.91f), Random.Range(-6f, -4f), 0);
        Debug.Log($"Instanciando tocha {indice} na posição {pos}");

        GameObject novaTocha = Instantiate(tochaAcessa, pos, Quaternion.identity);

        float valorAleatorio = Random.Range(5f, 6f);
        TochaData novaTochaData = new TochaData(novaTocha, valorAleatorio, indice);

        // Adiciona click
        TochasClickavel clickavel = novaTocha.AddComponent<TochasClickavel>();
        clickavel.DefinirTocha(novaTochaData);

        // Número visível da tocha
        TextMesh texto = novaTocha.AddComponent<TextMesh>();
        texto.text = indice.ToString();
        texto.characterSize = 0.2f;
        texto.color = Color.white;
        texto.transform.position += new Vector3(0, 0.5f, 0);

        tochasGeradas.Add(novaTochaData);
    }


    List<int> GerarOrdemCorreta()
    {
        List<int> indicesDisponiveis = new List<int>();
        for (int i = 0; i < tochasGeradas.Count; i++)
        {
            indicesDisponiveis.Add(i);
        }

        List<int> ordem = new List<int>();
        while (indicesDisponiveis.Count > 0)
        {
            int rand = Random.Range(0, indicesDisponiveis.Count);
            ordem.Add(indicesDisponiveis[rand]);
            indicesDisponiveis.RemoveAt(rand);
        }

        return ordem;
    }

    public void TochaClicada(int index, SpriteRenderer spriteRenderer)
    {
        if (temItem) return;

        cliquesDoJogador.Add(index);

        if (cliquesDoJogador.Count <= ordemCorreta.Count)
        {
            if (ordemCorreta[cliquesDoJogador.Count - 1] == index)
            {
                spriteRenderer.sprite = tochaApagada;

                if (cliquesDoJogador.Count == ordemCorreta.Count)
                {
                    temItem = true;
                    Debug.Log("Todas tochas clicadas corretamente! Item obtido.");

                    // Spawnar o item em uma posição no mapa
                    Instantiate(itemPremioPrefab, new Vector3(-35f, -4f), Quaternion.identity);
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

    IEnumerator PiscarTochasNaOrdem()
    {
        foreach (int indice in ordemCorreta)
        {
            GameObject tocha = tochasGeradas[indice].tocha;
            SpriteRenderer sr = tocha.GetComponent<SpriteRenderer>();

            sr.sprite = tochaApagada;
            yield return new WaitForSeconds(0.5f);
            sr.sprite = tochaAcessa.GetComponent<SpriteRenderer>().sprite;
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Chamado quando o jogador coleta o item
    public void MostrarItemNaUI()
    {
        if (imagemDoItemUI != null)
        {
            imagemDoItemUI.SetActive(true);
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

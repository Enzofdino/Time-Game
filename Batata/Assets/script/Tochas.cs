using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Tochas : MonoBehaviour
{
    public static Tochas instance;

    [SerializeField] GameObject tochaPrefab;
    [SerializeField] Sprite tochaApagada;
    [SerializeField] Sprite tochaErrada;
    [SerializeField] GameObject itemPremioPrefab;
    [SerializeField] GameObject imagemDoItemUI;

    public bool temItem = false;

    private List<TochaData> tochasGeradas = new List<TochaData>();
    private List<int> ordemCorreta = new List<int>();
    private List<int> cliquesDoJogador = new List<int>();

   public int quantidadeGerada = 5;
    float minX = -39.66f, maxX = -30.91f, minY = -6f, maxY = -4f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GerarTochas();
        ordemCorreta = GerarOrdemCorreta();
        StartCoroutine(PiscarTochasNaOrdem());
    }

    void GerarTochas()
    {
        List<Vector3> posicoesUsadas = new List<Vector3>();

        for (int i = 0; i < quantidadeGerada; i++)
        {
            Vector3 pos;
            int tentativas = 0;

            do
            {
                pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                tentativas++;
            } while (PosicaoMuitoPerto(pos, posicoesUsadas) && tentativas < 100);

            posicoesUsadas.Add(pos);
            GameObject novaTocha = Instantiate(tochaPrefab, pos, Quaternion.identity);
            TochaData novaTochaData = new TochaData(novaTocha, Random.Range(5f, 6f), i);

            // Adiciona click
            TochasClickavel clickavel = novaTocha.AddComponent<TochasClickavel>();
            clickavel.DefinirTocha(novaTochaData);

            // Número visível
            TextMesh texto = novaTocha.AddComponent<TextMesh>();
            texto.text = i.ToString();
            texto.characterSize = 0.2f;
            texto.color = Color.white;
            texto.transform.position += new Vector3(0, 0.5f, 0);

            tochasGeradas.Add(novaTochaData);
        }
    }

    bool PosicaoMuitoPerto(Vector3 nova, List<Vector3> existentes)
    {
        foreach (var pos in existentes)
        {
            if (Vector3.Distance(nova, pos) < 1.2f)
                return true;
        }
        return false;
    }

    List<int> GerarOrdemCorreta()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < tochasGeradas.Count; i++)
            indices.Add(i);

        List<int> ordem = new List<int>();
        while (indices.Count > 0)
        {
            int rand = Random.Range(0, indices.Count);
            ordem.Add(indices[rand]);
            indices.RemoveAt(rand);
        }
        return ordem;
    }

    public void TochaClicada(int index, SpriteRenderer sr)
    {
        if (temItem) return;

        cliquesDoJogador.Add(index);
        if (ordemCorreta[cliquesDoJogador.Count - 1] == index)
        {
            sr.sprite = tochaApagada;

            if (cliquesDoJogador.Count == ordemCorreta.Count)
            {
                temItem = true;
                Debug.Log("Sequência correta! Item liberado.");
                Instantiate(itemPremioPrefab, new Vector3(-35f, -4f), Quaternion.identity);
            }
        }
        else
        {
            sr.sprite = tochaErrada;
            cliquesDoJogador.Clear();
            Debug.Log("Sequência errada. Reiniciando...");
        }
    }

    IEnumerator PiscarTochasNaOrdem()
    {
        foreach (int idx in ordemCorreta)
        {
            GameObject tocha = tochasGeradas[idx].tocha;
            SpriteRenderer sr = tocha.GetComponent<SpriteRenderer>();

            sr.sprite = tochaApagada;
            yield return new WaitForSeconds(0.5f);
            sr.sprite = tochaPrefab.GetComponent<SpriteRenderer>().sprite;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void MostrarItemNaUI()
    {
        if (imagemDoItemUI != null)
            imagemDoItemUI.SetActive(true);
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

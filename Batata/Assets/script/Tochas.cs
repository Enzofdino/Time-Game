using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tochas : MonoBehaviour
{
    #region singleton
    public static Tochas instance;
    #endregion

    [SerializeField]
    public GameObject tochaAcessa;  // Sprite da tocha acesa
    [SerializeField]
    public Sprite tochaApagada; // Sprite da tocha apagada
    [SerializeField]
    public Sprite tochaErrada;  // Sprite da tocha errada

    List<TochaData> tochasGeradas = new List<TochaData>(); // Lista para armazenar as tochas geradas e seus valores

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
            GerarTochas();
        }
    }

    void GerarTochas()
    {
        // Instancia uma nova tocha com um valor aleatório entre 0 e 1
        GameObject novaTocha = Instantiate(tochaAcessa, new Vector3(Random.Range(-69.77f, -35), -0.9f), Quaternion.identity);
        float valorAleatorio = Random.Range(0f, 1f);
        TochaData novaTochaData = new TochaData(novaTocha, valorAleatorio);

        
        TochaClickavel clickavel = novaTocha.AddComponent<TochaClickavel>();
        clickavel.DefinirTocha(novaTochaData);

        tochasGeradas.Add(novaTochaData);
    }
}


[System.Serializable]
public class TochaData
{
    public GameObject tocha; 
    public float valor;

    public TochaData(GameObject tocha, float valor)
    {
        this.tocha = tocha;
        this.valor = valor;
    }
}


public class TochaClickavel : MonoBehaviour
{
    private TochaData tochaData;
    private SpriteRenderer spriteRenderer;

    public void DefinirTocha(TochaData tocha)
    {
        this.tochaData = tocha;
        this.spriteRenderer = tocha.tocha.GetComponent<SpriteRenderer>();
    }

    private void OnMouseUpAsButton()
    {
        
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado!");
            return;
        }


        if (tochaData.valor < 0.5f)
        {
            spriteRenderer.sprite = Tochas.instance.tochaApagada;
        }
        else
        {
            spriteRenderer.sprite = Tochas.instance.tochaErrada;
        }

        Debug.Log("Tocha clicada, valor: " + tochaData.valor);
    }
}

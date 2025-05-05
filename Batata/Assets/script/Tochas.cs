using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tochas : MonoBehaviour
{
    [Header("Prefabs de Tochas")]
    [SerializeField] GameObject tochaAcesaPrefab;
    [SerializeField] GameObject tochaApagadaPrefab;
    [SerializeField] GameObject tochaErradaPrefab;

    public int quantidadeTochas = 5;

    float minX = -39.66f, maxX = -30.91f;
    float minY = -6f, maxY = -4f;

    List<Vector3> posicoesUsadas = new List<Vector3>();
    List<GameObject> tochas = new List<GameObject>();
    List<int> ordemCorreta = new List<int>();
    List<int> cliquesDoJogador = new List<int>();

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

        int qtdCorretas = Random.Range(1, quantidadeTochas - 1); // pelo menos 1 correta
        int qtdErradas = Random.Range(0, quantidadeTochas - qtdCorretas);
        int qtdApagadas = quantidadeTochas - qtdCorretas - qtdErradas;

        List<string> tipos = new List<string>();

        for (int i = 0; i < qtdCorretas; i++) tipos.Add("correta");
        for (int i = 0; i < qtdErradas; i++) tipos.Add("errada");
        for (int i = 0; i < qtdApagadas; i++) tipos.Add("apagada");

        // Embaralhar tipos
        for (int i = 0; i < tipos.Count; i++)
        {
            string temp = tipos[i];
            int randomIndex = Random.Range(i, tipos.Count);
            tipos[i] = tipos[randomIndex];
            tipos[randomIndex] = temp;
        }

        for (int i = 0; i < quantidadeTochas; i++)
        {
            Vector3 pos;
            int tentativas = 0;

            do
            {
                pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                tentativas++;
            } while (PosicaoMuitoPerto(pos) && tentativas < 100);

            posicoesUsadas.Add(pos);

            GameObject prefab = null;
            switch (tipos[i])
            {
                case "correta":
                    prefab = tochaAcesaPrefab;
                    break;
                case "apagada":
                    prefab = tochaApagadaPrefab;
                    break;
                case "errada":
                    prefab = tochaErradaPrefab;
                    break;
            }

            GameObject tocha = Instantiate(prefab, pos, Quaternion.identity);
            tocha.AddComponent<BoxCollider2D>();

            // Só as corretas podem ser clicadas com ordem
            if (tipos[i] == "correta")
            {
                TochaClickavel click = tocha.AddComponent<TochaClickavel>();
                click.Definir(this, i); // i será usado como índice da ordem
                ordemCorreta.Add(i);
            }

            tochas.Add(tocha);
        }

        Debug.Log("Quantidade: " + quantidadeTochas + " | Corretas: " + qtdCorretas + " | Erradas: " + qtdErradas + " | Apagadas: " + qtdApagadas);
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
        // Ordem já é montada em SpawnarTochas
        // Embaralhar a ordem correta
        for (int i = 0; i < ordemCorreta.Count; i++)
        {
            int rand = Random.Range(i, ordemCorreta.Count);
            int temp = ordemCorreta[i];
            ordemCorreta[i] = ordemCorreta[rand];
            ordemCorreta[rand] = temp;
        }

        Debug.Log("Ordem correta: " + string.Join(", ", ordemCorreta));
    }

    public void TentarClicar(int index)
    {
        if (portaoAberto) return;

        cliquesDoJogador.Add(index);

        if (ordemCorreta[cliquesDoJogador.Count - 1] == index)
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
    }

    void ResetarTochas()
    {
        cliquesDoJogador.Clear();
        ordemCorreta.Clear();

        foreach (GameObject t in tochas)
            Destroy(t);

        SpawnarTochas();
        GerarOrdemCorreta();
    }
}

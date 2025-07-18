using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SinoManager : MonoBehaviour
{
    [Header("Referência dos Sinos")]
    public List<SinoClickavel> sinos;

    [Header("Tilemap a ser ativado após o sucesso")]
    public Tilemap tilemapParaAtivar;

    [Header("Ordem correta pré-definida")]
    public List<string> ordemCorreta = new List<string> { "Preto", "Verde", "Laranja", "Vermelho", "Amarelo" };

    private List<string> ordemDoJogador = new List<string>();
    private bool puzzleResolvido = false;

    void Start()
    {
        OcultarTilemap();
        Debug.Log("🧩 Ordem correta dos sinos: " + string.Join(" -> ", ordemCorreta));
    }

    void OcultarTilemap()
    {
        if (tilemapParaAtivar != null)
            tilemapParaAtivar.gameObject.SetActive(false);
    }

    void AtivarTilemap()
    {
        if (tilemapParaAtivar != null)
            tilemapParaAtivar.gameObject.SetActive(true);
    }

    public void SinoTocado(string cor)
    {
        if (puzzleResolvido)
        {
            Debug.Log("⛔ Puzzle já resolvido. Ignorando toque no sino: " + cor);
            return;
        }

        ordemDoJogador.Add(cor);
        Debug.Log("🔔 Sino tocado: " + cor + " | Posição na sequência: " + ordemDoJogador.Count);

        int idx = ordemDoJogador.Count - 1;

        if (ordemCorreta[idx] == cor)
        {
            Debug.Log("✅ Cor correta! Esperado: " + ordemCorreta[idx]);

            if (ordemDoJogador.Count == ordemCorreta.Count)
            {
                Debug.Log("🎉 Puzzle dos sinos resolvido! Ativando tilemap...");
                puzzleResolvido = true;
                AtivarTilemap();
            }
        }
        else
        {
            Debug.Log("❌ Cor ERRADA! Tocou: " + cor + " | Esperado: " + ordemCorreta[idx]);
            ordemDoJogador.Clear();
        }
    }
}

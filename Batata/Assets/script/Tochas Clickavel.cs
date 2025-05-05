using UnityEngine;

public class TochaClickavel : MonoBehaviour
{
    private Tochas gerenciador;
    private string cor;

    // Define o gerenciador e a cor da tocha
    public void Definir(Tochas t, string corTocha)
    {
        gerenciador = t;
        cor = corTocha;
    }

    // Ao clicar na tocha, tenta ativar com base na cor
    void OnMouseDown()
    {
        gerenciador.TentarClicar(cor);
    }
}

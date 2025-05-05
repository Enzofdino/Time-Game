using UnityEngine;

public class TochaClickavel : MonoBehaviour
{
    private Tochas gerenciador;
    private int indice;

    public void Definir(Tochas t, int idx)
    {
        gerenciador = t;
        indice = idx;
    }

    void OnMouseDown()
    {
        gerenciador.TentarClicar(indice);
    }
}

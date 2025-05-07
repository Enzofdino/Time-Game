using UnityEngine;
using System.Collections;


public class TochaClickavel : MonoBehaviour
{
    string cor;
    Tochas gerenciador;

    public void Definir(Tochas t, string c)
    {
        gerenciador = t;
        cor = c;
    }

    public string GetCor()
    {
        return cor;
    }

    void OnMouseDown()
    {
        if (gerenciador != null)
        {
            gerenciador.TentarClicar(cor);
        }
        else
        {
            Debug.LogError("Gerenciador de tochas não foi definido!");
        }
    }

    public IEnumerator Piscar()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color corOriginal = sr.color;

        sr.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sr.color = corOriginal;
    }
}

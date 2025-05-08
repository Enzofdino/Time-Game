using System.Collections;
using UnityEngine;

public class TochaClickavel : MonoBehaviour
{
    private Tochas gerenciador;
    private string cor;

    public void Definir(Tochas t, string c)
    {
        gerenciador = t;
        cor = c;
    }

    public string GetCor() // ← ESSA PARTE É IMPORTANTE
    {
        return cor;
    }

    private void OnMouseDown()
    {
        if (gerenciador != null)
        {
            gerenciador.TentarClicar(cor);
        }
    }

    public IEnumerator Piscar()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        sr.color = originalColor;
    }
}

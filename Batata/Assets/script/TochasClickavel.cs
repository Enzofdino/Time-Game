using UnityEngine;

public class TochasClickavel : MonoBehaviour
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
        if (spriteRenderer == null) return;

        Tochas.instance.TochaClicada(tochaData.index, spriteRenderer);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Energiabarra : MonoBehaviour
{
    public Jump playerJump; // Referência ao script do jogador
    public Image energiaBarra; // A Image da barra
    public Text energiaTexto;

    void Update()
    {
        if (playerJump != null && energiaBarra != null)
        {
            energiaBarra.fillAmount = playerJump.energy / 6f; // valor entre 0 e 1
            energiaTexto.text = "Energia: " + playerJump.energy + " / 6";
        }
    }
}




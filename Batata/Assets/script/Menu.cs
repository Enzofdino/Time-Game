using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class MenuPrincipal : MonoBehaviour
{
    public void Jogar()
    {
        
        // Troca para a cena do jogo (coloque o nome exato da cena)
        SceneManager.LoadScene("Mapa0.5");
    }

    public void Sair()
    {
        // Fecha o jogo (funciona só no executável)
        Application.Quit();
        Debug.Log("Jogo fechado."); // Para teste no editor
    }

}
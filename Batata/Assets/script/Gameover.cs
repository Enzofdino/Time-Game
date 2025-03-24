using UnityEngine;

public class Gameover : MonoBehaviour
{
    public static Gameover instance;
    [SerializeField] private GameObject gameOverCanvas; // Arraste o Canvas de Game Over na Unity

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameOverCanvas.SetActive(false); // Garante que o Canvas esteja invisível no início
    }

    public void AtivarGameOver()
    {
        if (gameOverCanvas != null)
        {
            Time.timeScale = 1f; // Garante que o Canvas atualize
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f; // Agora pausa o jogo
            Debug.Log("Game Over!");
        }
        else
        {
            Debug.LogError("GameOver Canvas não está atribuído!");
        }
    }
    
}

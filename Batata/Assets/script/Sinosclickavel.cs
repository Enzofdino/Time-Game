using UnityEngine;

public class SinoClickavel : MonoBehaviour
{
    public string corDoSino; // Ex: "Preto", "Verde", etc.
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        Debug.Log("🎯 Clique no sino: " + corDoSino);

        // Tocar som
        if (audioSource != null)
            audioSource.Play();

        // Informar ao manager
        SinoManager manager = FindObjectOfType<SinoManager>();
        if (manager != null)
        {
            manager.SinoTocado(corDoSino);
        }
        else
        {
            Debug.LogWarning("SinoManager não encontrado na cena!");
        }
    }
}

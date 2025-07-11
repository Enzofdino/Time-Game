using System.Collections;
using UnityEngine;

public class PortaTemporizada : MonoBehaviour
{
    public PortaTemporizada instance;

    [Header("Referências de Porta")]
    [SerializeField] private GameObject portaFechada;  // GameObject da porta fechada
    [SerializeField] private GameObject portaAberta;   // GameObject da porta aberta

    private Collider2D col;

    private void Awake()
    {
        instance = this;
        col = GetComponent<Collider2D>();
    }

    public void AbrirTemporariamente(float duracao)
    {
        StartCoroutine(AbrirFecharPorta(duracao));
    }

    IEnumerator AbrirFecharPorta(float tempo)
    {
        Debug.Log("Porta aberta");

        if (portaFechada != null) portaFechada.SetActive(false);
        if (portaAberta != null) portaAberta.SetActive(true);
        col.enabled = false;

        yield return new WaitForSeconds(tempo);

        Debug.Log("Porta fechada");
        if (portaFechada != null) portaFechada.SetActive(true);
        if (portaAberta != null) portaAberta.SetActive(false);
        col.enabled = true;
    }
}

using UnityEngine;

public class PlacaDePressao : MonoBehaviour
{
    [SerializeField] private GameObject portaObjeto; // Referência ao GameObject da porta
    [SerializeField] private float tempoAberta = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Pegamos o script "PortaTemporizada" no GameObject
            PortaTemporizada porta = portaObjeto.GetComponent<PortaTemporizada>();

            if (porta != null)
            {
                porta.AbrirTemporariamente(tempoAberta);
            }
            else
            {
                Debug.LogError("O GameObject da porta não tem o script PortaTemporizada!");
            }
        }
    }
}

using UnityEngine;

public class ReceberLaser : MonoBehaviour
{
    public GameObject portao;

    public void Ativar()
    {
        portao.SetActive(false); // Portão desaparece ou abre
    }
}

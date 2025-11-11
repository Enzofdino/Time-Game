using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    public GameObject portao;

    public void Ativar()
    {
        portao.SetActive(false); // Portão desaparece ou abre
    }
}

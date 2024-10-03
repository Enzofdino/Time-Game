using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Portas : MonoBehaviour
{
    bool portafechada;
    GameObject portaaberta;
    bool question1;
    bool questioncerta;
    GameObject jogador;

   

    public void OnCollisionEnter2D(Collision2D collision, GameObject porta)
    {
        if (jogador = porta)
        {
            collision.gameObject.SetActive(true);
            Debug.Log("Jogador colidiu com a porta");
        }
    }
    public void Interagirporta(bool acertou)
    {
        
        if(Input.GetKeyUp(KeyCode.E))
        {
            portaaberta.SetActive(true);
        }
        
    }
    
    public void Perguntas(TextMeshProUGUI proximaquestion)
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            portafechada = true;
            if(question1 == true)
            {
                proximaquestion.ToString();            
            }
            else if(questioncerta == false)
            {
                question1 = true;
            }
        }
        
    }

   
}

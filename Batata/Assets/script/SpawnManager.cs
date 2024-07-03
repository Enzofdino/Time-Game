using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] structurePrim;
    [SerializeField]
    GameObject[] structureMed;
    [SerializeField]
    GameObject[] structureCont;
    [SerializeField]
    GameObject[] structureMod;

    bool stPrim;
    bool stMed;
    bool stCont;
    bool stMod;
    GameObject structure;

    private void Start()
    {
        stPrim = true;
        stMed = false;
        stCont = false;
        stMod = false;

    }
    private void Update()
    {
        if ((Contador.instance.era == "PréHistórica")&&(stPrim == true))
        {
            structure = Instantiate(structurePrim[0]);
            stPrim = false;
            stMed = true;
        }
        if ((Contador.instance.era == "Medieval")&& (stMed == true))
        {
            Destroy(structure);
            structure = Instantiate(structureMed[0]);
            stMed = false;
            stCont = true;
        }
        if ((Contador.instance.era == "Contemporânea")&&(stCont == true))
        {
            Destroy(structure);
            structure = Instantiate(structureCont[0]);
            stCont = false;
            stMod = true;
        }
        if ((Contador.instance.era== "Moderna")&&(stMod == true))
        {
            Destroy(structure);
            structure = Instantiate(structureMod[0]);
            stMod = false;
        }





    }

    //identificar cada prefab para cada era (FEITO)


    //definir uma área para gerar o prefab  (A FAZER)


    //instanciar o prefab, dentro da área definida, apenas se não houver outro prefab na área (A FAZER)


    //checar se o boxcolider dos prefabs estão distantes um do outro (A FAZER)


    //verificar se a boxcolider esta em contato com o chão (A FAZER)


    //caso o prefab gere dentro do chão, destruir ele (A FAZER)


    //ao mudar de era, substituir os prefabs pelos prefabs da era seguinte (FEITO)
}

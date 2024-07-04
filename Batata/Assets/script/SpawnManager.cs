using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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

    int stuPrim;
    int stuMed;
    int stuCont;
    int stuMod;

    int contPrim;
    int contMed;
    int contMod;
    int contCont;

    private GameObject[] spawnedPrimStructures;
    private GameObject[] spawnedMedStructures;
    private GameObject[] spawnedContStructures;
    private GameObject[] spawnedModStructures;

    private void Start()
    {
        stPrim = true;
        stMed = false;
        stCont = false;
        stMod = false;

        contPrim = 0;
        contMed = 0;
        contCont = 0;
        contMod = 0;

        stuPrim = Random.Range(4, 10);
        stuMed = Random.Range(4, 10);
        stuCont = Random.Range(4, 10);
        stuMod = Random.Range(4, 10);

        spawnedPrimStructures = new GameObject[stuPrim];
        spawnedMedStructures = new GameObject[stuMed];
        spawnedContStructures = new GameObject[stuCont];
        spawnedModStructures = new GameObject[stuMod];
    }
    private void Update()
    {
        if ((Contador.instance.era == "PréHistórica") && (stPrim == true) && (contPrim != stuPrim))
        {

            do
            {  
                int lugar = 0;
                bool collider = true;
                while (collider)
                {
                    lugar = Random.Range(31, 104);
                    collider = Physics2D.OverlapBox(new Vector2(lugar, 0f), new Vector2(6.17f, 3.81f), 0);
                }
                structure = Instantiate(structurePrim[0], new Vector3(lugar, 0f, 0f), Quaternion.identity);
                spawnedPrimStructures[contPrim] = structure;
                contPrim++;
            }
            while (contPrim != stuPrim);
            stMed = true;
            stPrim = false;


        }
        if ((Contador.instance.era == "Medieval") && (stMed == true) && (contMed != stuMed))
        {
            DestroyAllPrimStructures();
            do
            {
                int lugar = 0;
                bool collider = true;
                while (collider)
                {
                    lugar = Random.Range(31, 104);
                    collider = Physics2D.OverlapBox(new Vector2(lugar, 0f), new Vector2(7.86f, 7.05f), 0);
                }
                structure = Instantiate(structureMed[0], new Vector3(lugar, 0f, 0f), Quaternion.identity);
                spawnedMedStructures[contMed] = structure;
                contMed++;
            }
            while (contMed != stuMed);
            stMed = false;
            stCont = true;
        }
        if ((Contador.instance.era == "Contemporânea") && (stCont == true) && (contCont != stuCont))
        {
            DestroyAllMedStructures();
            do
            {
                int lugar = 0;
                bool collider = true;
                while (collider)
                {
                    lugar = Random.Range(31, 104);
                    collider = Physics2D.OverlapBox(new Vector2(lugar, 0f), new Vector2(8.9f, 10f), 0);
                }
                structure = Instantiate(structureCont[0], new Vector3(lugar, 0f, 0f), Quaternion.identity);
                spawnedContStructures[contCont] = structure;
                contCont++;
            }
            while (contCont != stuCont);
            stCont = false;
            stMod = true;
        }
        if ((Contador.instance.era == "Moderna") && (stMod == true) && (contMod != stuMod))
        {
            DestroyAllContStructures();
            do
            {
                int lugar = 0;
                bool collider = true;
                while (collider)
                {
                    lugar = Random.Range(31, 104);
                    collider = Physics2D.OverlapBox(new Vector2(lugar, 0f), new Vector2(8.2f, 19.1f), 0);
                }
                structure = Instantiate(structureMod[0], new Vector3(lugar, 0f, 0f), Quaternion.identity);
                spawnedModStructures[contMod] = structure;
                contMod++;
            }
            while (contMod != stuMod);
            stMod = false;
        }
    }
        private void DestroyAllPrimStructures()
        {
            for (int i = 0; i < contPrim; i++)
            {
                Destroy(spawnedPrimStructures[i]);
            }
            contPrim = 0;
        }

        private void DestroyAllMedStructures()
        {
            for (int i = 0; i < contMed; i++)
            {
                Destroy(spawnedMedStructures[i]);
            }
            contMed = 0;
        }

        private void DestroyAllContStructures()
        {
            for (int i = 0; i < contCont; i++)
            {
                Destroy(spawnedContStructures[i]);
            }
            contCont = 0;
        }

        private void DestroyAllModStructures()
        {
            for (int i = 0; i < contMod; i++)
            {
                Destroy(spawnedModStructures[i]);
            }
            contMod = 0;
        }
    }       






    //identificar cada prefab para cada era (FEITO)


    //definir uma área para gerar o prefab  (Feito)


    //instanciar o prefab, dentro da área definida, apenas se não houver outro prefab na área (A FAZER)


    //checar se o boxcolider dos prefabs estão distantes um do outro (A FAZER)


    //verificar se a boxcolider esta em contato com o chão (A FAZER)


    //caso o prefab gere dentro do chão, destruir ele (A FAZER)


    //ao mudar de era, substituir os prefabs pelos prefabs da era seguinte (FEITO)


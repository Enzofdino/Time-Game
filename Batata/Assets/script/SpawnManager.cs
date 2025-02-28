using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    #region singleton
    bool spawnPrimitivo = true;
    bool spawnMedieval = true;
    bool spawnContemporaneo = true;
    bool spawnModerno = true;
    #endregion

    public GameObject[] primitivePrefabs;
    public GameObject[] medievalPrefabs;
    public GameObject[] contemporaryPrefabs;
    public GameObject[] modernPrefabs;

    private GameObject[] currentPrefabArray;
    private GameObject[] spawnedInstances;
    private Vector3[] instancePositions;

    private int numberOfInstances;
    private int currentRound = 0;

    float heightPoss = -1.65f;
    float minDistance = 5f; // Dist�ncia m�nima entre as inst�ncias

    void Start()
    {
        currentPrefabArray = primitivePrefabs;
        InstantiateObjects();
    }

    void Update()
    {
        if (Contador.instance.ano == 10 && spawnMedieval == true)
        {
            DestroyAllInstances();
            NextRound();
            spawnMedieval = false;
        }
        if (Contador.instance.ano == 11 && spawnContemporaneo == true)
        {
            DestroyAllInstances();
            NextRound();
            spawnContemporaneo = false;
        }
        if (Contador.instance.ano == 12 && spawnModerno == true)
        {
            DestroyAllInstances();
            NextRound();
            spawnModerno = false;
        }
    }

    void InstantiateObjects()
    {
        // Set the number of instances to a random value between 4 and 8
        numberOfInstances = Random.Range(4, 9);

        // Initialize the spawnedInstances array based on the random number of instances
        spawnedInstances = new GameObject[numberOfInstances];
        instancePositions = new Vector3[numberOfInstances]; // Armazena as posi��es das inst�ncias

        // Instantiate prefabs from the currentPrefabArray
        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 newPosition;
            bool validPosition = false;

            // Tentar encontrar uma posi��o v�lida que n�o sobreponha outra
            do
            {
                // Gerar uma nova posi��o aleat�ria
                newPosition = new Vector3(Random.Range(38.61f, 106.8f), heightPoss, 1f);

                // Verificar se a posi��o � v�lida
                validPosition = IsPositionValid(newPosition, i);

            } while (!validPosition); // Continuar at� encontrar uma posi��o v�lida

            // Instanciar o objeto na posi��o v�lida
            int randomIndex = Random.Range(0, currentPrefabArray.Length);
            GameObject instance = Instantiate(currentPrefabArray[randomIndex], newPosition, quaternion.identity);

            // Armazenar a inst�ncia e sua posi��o
            spawnedInstances[i] = instance;
            instancePositions[i] = newPosition;
        }
    }

    // Fun��o para verificar se a posi��o � v�lida (n�o sobrep�e outra)
    bool IsPositionValid(Vector3 newPosition, int currentIndex)
    {
        for (int i = 0; i < currentIndex; i++)
        {
            // Verificar se a dist�ncia entre a nova posi��o e as posi��es anteriores � suficiente
            if (Vector3.Distance(newPosition, instancePositions[i]) < minDistance)
            {
                return false; // Posi��o inv�lida, muito perto de outra inst�ncia
            }
        }
        return true; // Posi��o v�lida
    }

    void DestroyAllInstances()
    {
        // Destroy all objects in the current round
        for (int i = 0; i < spawnedInstances.Length; i++)
        {
            if (spawnedInstances[i] != null)
            {
                Destroy(spawnedInstances[i]);
                spawnedInstances[i] = null;
            }
        }
    }

    void NextRound()
    {
        // Increment the round counter
        currentRound++;

        // Switch to the next array based on the round in the specified order
        if (currentRound == 1)
        {
            currentPrefabArray = medievalPrefabs;  // Switch to medieval prefabs
            heightPoss = -1.18f;
        }
        else if (currentRound == 2)
        {
            currentPrefabArray = contemporaryPrefabs;  // Switch to contemporary prefabs
            heightPoss = 0.35f;
        }
        else if (currentRound == 3)
        {
            currentPrefabArray = modernPrefabs;    // Switch to modern prefabs
            heightPoss = 4.8f;
        }
        else
        {
            currentRound = 0; // Loop back to the first round (primitive)
            currentPrefabArray = primitivePrefabs;
            heightPoss = -1.61f;
        }

        // Instantiate objects for the next round
        InstantiateObjects();
    }
}
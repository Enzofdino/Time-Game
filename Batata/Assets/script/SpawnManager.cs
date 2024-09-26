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

    // Current array being used for instantiation
    private GameObject[] currentPrefabArray;
    private GameObject[] spawnedInstances;

    // Number of instances will be set randomly between 4 and 8
    private int numberOfInstances;

    // Round counter
    private int currentRound = 0;

    float heightPoss = -1.65f;

    void Start()
    {
        // Start with the first array of prefabs (primitive)
        currentPrefabArray = primitivePrefabs;
        InstantiateObjects();
    }

    void Update()


    {
        // Press 'D' to destroy current instances and move to the next round
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
            spawnModerno =false;
        }
    }

    void InstantiateObjects()
    {
        // Set the number of instances to a random value between 4 and 8
        numberOfInstances = Random.Range(4, 9);

        // Initialize the spawnedInstances array based on the random number of instances
        spawnedInstances = new GameObject[numberOfInstances];

        // Instantiate prefabs from the currentPrefabArray
        for (int i = 0; i < numberOfInstances; i++)
        {
            // Randomly select a prefab from the currentPrefabArray
            int randomIndex = Random.Range(0, currentPrefabArray.Length);

            // Instantiate at a random position
            GameObject instance = Instantiate(currentPrefabArray[randomIndex],
                                              new Vector3(Random.Range(38.61f, 106.8f), heightPoss , 1f), quaternion.identity);

            // Store the instance in the spawnedInstances array
            spawnedInstances[i] = instance;
        }
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
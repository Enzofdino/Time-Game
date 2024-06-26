using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    public enum Era
    {
        Prehistoric,
        Medieval,
        Contemporary,
        Modern
    }

    public Era currentEra = Era.Prehistoric;

    public GameObject[] prehistoricStructures;
    public GameObject[] medievalStructures;
    public GameObject[] contemporaryStructures;
    public GameObject[] modernStructures;

    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;

    public LayerMask groundLayer; // Layer mask for ground objects

    void Start()
    {
        SpawnStructures();
    }

    void Update()
    {
        // Example: Switch era with keyboard input for testing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentEra = Era.Prehistoric;
            SpawnStructures();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentEra = Era.Medieval;
            SpawnStructures();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentEra = Era.Contemporary;
            SpawnStructures();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentEra = Era.Modern;
            SpawnStructures();
        }
    }

    void SpawnStructures()
    {
        ClearPreviousStructures();

        switch (currentEra)
        {
            case Era.Prehistoric:
                SpawnPrefabArray(prehistoricStructures);
                break;
            case Era.Medieval:
                SpawnPrefabArray(medievalStructures);
                break;
            case Era.Contemporary:
                SpawnPrefabArray(contemporaryStructures);
                break;
            case Era.Modern:
                SpawnPrefabArray(modernStructures);
                break;
            default:
                Debug.LogError("Unknown era selected.");
                break;
        }
    }

    void SpawnPrefabArray(GameObject[] prefabArray)
    {
        foreach (GameObject structure in prefabArray)
        {
            Vector3 spawnPosition = GetGroundedSpawnPosition();
            Instantiate(structure, spawnPosition, Quaternion.identity);
        }
    }

    void ClearPreviousStructures()
    {
        // Implement clearing of previously spawned structures if needed
        // Example: Destroy existing structure game objects
        GameObject[] existingStructures = GameObject.FindGameObjectsWithTag("Structure");
        foreach (GameObject structure in existingStructures)
        {
            Destroy(structure);
        }
    }

    Vector3 GetGroundedSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(spawnAreaCenter, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            spawnPosition = hit.point;
        }
        else
        {
            Debug.LogWarning("Failed to find ground. Defaulting to spawn area center.");
            spawnPosition = spawnAreaCenter;
        }

        return spawnPosition;
    }

    // Additional methods for adjusting spawn area, handling collisions, etc.
}
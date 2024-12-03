using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UrkaiSpawner : MonoBehaviour
{
    // Prefab to spawn
    public GameObject urkaiPrefab;

    // Spawn point
    public Transform spawnPoint;

    // Timing variables
    private float spawnTimer;
    private float spawnInterval;

    void Update()
    {
        spawnInterval = Random.Range(2f, 5f);
        // Increment the timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn
        if (spawnTimer >= spawnInterval)
        {
            SpawnUrkai();
            spawnTimer = 0f; // Reset the timer
        }
    }

    void SpawnUrkai()
    {
        // Instantiate the prefab at the spawn point
        Debug.Log("spawning)");
        Instantiate(urkaiPrefab, spawnPoint.position, Quaternion.identity);
    }
}

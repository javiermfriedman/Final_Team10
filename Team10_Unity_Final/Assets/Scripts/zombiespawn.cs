using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    // Object variables
    public GameObject treePrefab;
    public Transform[] spawnPoints;
    private int rangeEnd;
    private Transform spawnPoint;

    // Timing variables
    public float timeToSpawn = 3f; // Set to 20 seconds
    private float spawnTimer = 0f;

    // Proximity variables
    public Transform cat; // Reference to the cat's transform
    public float spawnRange = 10f; // Distance within which the spawner is active

    void Start()
    {
        // Assign the length of the array to the end of the random range
        rangeEnd = spawnPoints.Length - 1;

        // Find the cat (player) if not assigned
        if (cat == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            cat = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        // Check if the cat is within proximity
        if (cat != null && Vector3.Distance(transform.position, cat.position) <= spawnRange)
        {
            spawnTimer += Time.deltaTime; // Increment based on real-time seconds

            if (spawnTimer >= timeToSpawn)
            {
                spawnTree();
                spawnTimer = 0f;
            }
        }
    }

    void spawnTree()
    {
        int SPnum = Random.Range(0, rangeEnd);
        spawnPoint = spawnPoints[SPnum];
        Instantiate(treePrefab, spawnPoint.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the spawn range in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}

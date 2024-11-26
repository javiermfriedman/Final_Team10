using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour {

      //Object variables
      public GameObject treePrefab;
      public Transform[] spawnPoints;
      private int rangeEnd;
      private Transform spawnPoint;

      //Timing variables
      public float timeToSpawn = 3f; // Set to 20 seconds
      private float spawnTimer = 0f;

      void Start(){
            // Assign the length of the array to the end of the random range
            rangeEnd = spawnPoints.Length - 1;
      }

      void FixedUpdate(){
            spawnTimer += Time.deltaTime; // Increment based on real-time seconds

            if (spawnTimer >= timeToSpawn){
                  spawnTree();
                  spawnTimer = 0f;
            }
      }

      void spawnTree(){
            int SPnum = Random.Range(0, rangeEnd);
            spawnPoint = spawnPoints[SPnum];
            Instantiate(treePrefab, spawnPoint.position, Quaternion.identity);
      }
}

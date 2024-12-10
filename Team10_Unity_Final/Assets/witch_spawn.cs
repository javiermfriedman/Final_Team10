using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch_spawn : MonoBehaviour
{
    public Transform spawnPoint;       // Point where objects will spawn
    public GameObject zombie;
    public GameObject giant;
    public GameObject bats;
    public GameObject token;
    // Method to spawn a random prefab
    public void Spawn_zombie()
    {
        Instantiate(zombie, spawnPoint.position, spawnPoint.rotation);
    }

    public void Spawn_bats()
    {
        Instantiate(bats, spawnPoint.position, spawnPoint.rotation);
    }

    public void Spawn_giant()
    {
        Instantiate(giant, spawnPoint.position, spawnPoint.rotation);
    }

    public void spawn_token()
    {
        Instantiate(token, spawnPoint.position, spawnPoint.rotation);
    }
}
    
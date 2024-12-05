using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] treePrefab;
    public GameObject zombie;


    public void spawn_enemy(){

            Instantiate(zombie, treePrefab[0].transform);
            Instantiate(zombie, treePrefab[1].transform);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] treePrefab;
    public GameObject zombie;


    void Start()
    {
        StartCoroutine(spawnClock());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnClock() {
        while(true){
            int num = Random.Range(0, treePrefab.Length);
            Debug.Log(num);
            Instantiate(zombie, treePrefab[num].transform);
            yield return new WaitForSeconds(3);

        }
       
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch : MonoBehaviour
{
    public Transform target; 
    public witchSpawner spawner;

    public GameObject projectile;
    public GameObject firePoint;

    public float startTimeBtwShots = 2f;
    private float timeBtwShots;

    private bool canspawn;
    public float spawn_cooldown;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        canspawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, target.position);
        Debug.Log("dist to player: " + distToPlayer);

        if (distToPlayer < 10f){
            shoot();
            // if (timeBtwShots <= 0)
            // {
            //     shoot();
            //     timeBtwShots = startTimeBtwShots;
            // }
            // else
            // {
            //     timeBtwShots -= Time.deltaTime;
            // }

            if (distToPlayer < 7f && canspawn)
            {
                spawner.spawn_enemy();
                canspawn = false; // Prevent further spawns until the wait is complete
                StartCoroutine(WaitAfterSpawn());
            }
        }

    }

    private void shoot()
    {
        if (target == null) return; // Ensure the target exists

        // Calculate the direction to the target
        Vector3 directionToPlayer = (target.position - firePoint.transform.position).normalized;

        // Rotate the firePoint to look at the player
        firePoint.transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // Instantiate the projectile
        Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);

        Debug.Log("Shooting towards player at: " + target.position);
    }



    private IEnumerator WaitAfterSpawn()
    {
        Debug.Log("Waiting for 10 seconds...");
        yield return new WaitForSeconds(spawn_cooldown); // Wait for 10 seconds
        Debug.Log("10 seconds have passed!");
        canspawn = true; // Allow spawning again
    }
}

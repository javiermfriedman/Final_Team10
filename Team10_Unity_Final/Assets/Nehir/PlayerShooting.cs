using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //public Animator animator;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    void Start(){
        // idk what an animator is I guess we will have this later 
        //animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update(){
        if (Time.time >= nextAttackTime){
                //if (Input.GetKeyDown(KeyCode.Space))
                if (Input.GetAxis("Attack") > 0){
                    playerFire();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
        }
    }

    void playerFire(){
        // Before adding the mouse aim
        //animator.SetTrigger ("Fire");
        // Vector2 fwd = (firePoint.position - this.transform.position).normalized;
        // GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        // projectile.GetComponent<Rigidbody2D>().AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);

        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; 

        // Calculate the direction from the firePoint to the mouse position
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Add force to the projectile in the direction of the mouse position
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);

        // Destroy the projectile after 5 seconds 
        Destroy(projectile, 5f);
    }
}

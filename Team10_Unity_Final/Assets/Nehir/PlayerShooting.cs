using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
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
        if (Time.time >= nextAttackTime) {
            // Check for left mouse button click
            if (Input.GetMouseButtonDown(0)) { // 0 is the left mouse button
                playerFire();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void playerFire(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (mousePos - firePoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);

        // Ignore collision between the player and the projectile
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();

        if (projectileCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, projectileCollider);
        }

        Destroy(projectile, 5f);
    }
}

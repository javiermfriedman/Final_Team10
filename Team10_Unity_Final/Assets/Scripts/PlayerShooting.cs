// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerShooting : MonoBehaviour
// {
//     public Animator anim1;
//     public Animator anim2;
//     public Transform firePoint;
//     public GameObject projectilePrefab;
//     public float projectileSpeed = 10f;
//     public float attackRate = 2f;
//     private float nextAttackTime = 0f;

//     public SpriteToggle spriteToggle;

//     void Start(){
//         // idk what an animator is I guess we will have this later 
//         //anim1 = gameObject.GetComponentInChildren<Animator>();
        
//             spriteToggle = FindObjectOfType<SpriteToggle>();
//               if (spriteToggle == null)
//               {
//                      Debug.LogWarning("SpriteToggle component not found! Ghost mode may not work as expected.");
//               }
        
//     }

//     void Update(){
//         if (Time.time >= nextAttackTime) {
//             // Check for left mouse button click
//             if (Input.GetMouseButtonDown(0)) { // 0 is the left mouse button
//                 playerFire();
//                 anim1.SetTrigger("Attack");
//                 anim2.SetTrigger("Attack");
//                 nextAttackTime = Time.time + 1f / attackRate;
//             }
//         }
//     }

//     void playerFire(){

//         if (!spriteToggle.isGhostMode) {


//         Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         mousePos.z = 0;

//         Vector2 direction = (mousePos - firePoint.position).normalized;

//         GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

//         projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);

//         // Ignore collision between the player and the projectile
//         Collider2D playerCollider = GetComponent<Collider2D>();
//         Collider2D projectileCollider = projectile.GetComponent<Collider2D>();

//         if (projectileCollider != null && playerCollider != null)
//         {
//             Physics2D.IgnoreCollision(playerCollider, projectileCollider);
//         }

//         Destroy(projectile, 5f);
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public SpriteToggle spriteToggle;

    void Start()
    {
        // Initialize SpriteToggle reference
        spriteToggle = FindObjectOfType<SpriteToggle>();
        if (spriteToggle == null)
        {
            Debug.LogWarning("SpriteToggle component not found! Ghost mode may not work as expected.");
        }
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            // Check for "Attack" input (keyboard, mouse, or controller)
            if (Input.GetButtonDown("Attack"))
            {
                playerFire();

                // Trigger animations if animators are assigned
                if (anim1 != null) anim1.SetTrigger("Attack");
                if (anim2 != null) anim2.SetTrigger("Attack");

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void playerFire()
    {
        if (!spriteToggle.isGhostMode)
        {
            // Determine aim direction
            Vector2 aimDirection = GetAimDirection();

            // Instantiate and fire the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce(aimDirection * projectileSpeed, ForceMode2D.Impulse);

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

    Vector2 GetAimDirection()
    {
        // Controller aiming (right analog stick)
        float horizontal = Input.GetAxis("HorizontalRightStick");
        float vertical = Input.GetAxis("VerticalRightStick");
        Vector2 controllerDirection = new Vector2(horizontal, vertical);

        if (controllerDirection.magnitude > 0.1f)
        {
            // Normalize direction for consistent movement speed
            return controllerDirection.normalized;
        }

        // Mouse aiming (fallback)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure 2D space
        return (mousePos - firePoint.position).normalized;
    }
}

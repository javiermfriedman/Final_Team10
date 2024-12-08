using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveShoot : MonoBehaviour {

      //public Animator anim;
       public float speed = 2f;
       public float stoppingDistance = 5f; // when enemy stops moving towards player
       public float retreatDistance = 4f; // when enemy moves away from approaching player
       private float timeBtwShots;
       public float startTimeBtwShots = 2;
       public GameObject projectile;

       private bool spawnGiant = true;

       public GameObject zombie;
       public GameObject giant;

       private bool spawn_ghost;

       private Rigidbody2D rb;
       private Transform player;
       private Vector2 PlayerVect;

       [SerializeField] floatingHealthBar healthBar;

       public int enemyMaxHealth = 30;
       public int enemyCurrHealth = 30;
       private Renderer rend;
       private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

       public GameObject ghost;

       //
        private bool canspawn;
        public float spawn_cooldown;
        public witchSpawner spawner;

       private SpriteToggle spriteToggle;

       void Start () {
              Physics2D.queriesStartInColliders = false;
              scaleX = gameObject.transform.localScale.x;

              rb = GetComponent<Rigidbody2D> ();
              player = GameObject.FindGameObjectWithTag("Player").transform;
              PlayerVect = player.transform.position;

              timeBtwShots = startTimeBtwShots;

              enemyCurrHealth = enemyMaxHealth;

              rend = GetComponentInChildren<Renderer> ();
              spriteToggle = FindObjectOfType<SpriteToggle>();
              healthBar = GetComponentInChildren<floatingHealthBar>();
              //anim = GetComponentInChildren<Animator> ();


              //if (GameObject.FindWithTag ("GameHandler") != null) {
              // gameHander = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
              //}
              spawn_ghost = true;
              canspawn = true;
              rb.constraints = RigidbodyConstraints2D.FreezeRotation;
       }

       void Update () {

 
              float DistToPlayer = Vector3.Distance(transform.position, player.position);
              if (DistToPlayer <= attackRange) {
                     
                     if (enemyCurrHealth == 3) {
                            if(spawnGiant){
                                   Instantiate(giant, gameObject.transform);
                                   spawnGiant = false;
                            } 
                            
                     }
                     //what happens when cat goes into ghost mode
                     if (spriteToggle.isGhostMode) {
                            if( spawn_ghost ) {
                                   summonGhosts();
                                   spawn_ghost = false;
                            }
                     } else {
                            spawn_ghost = true;
                                                 //spawn in zomvies if too close
                            if (DistToPlayer < 7f && canspawn)
                            {
                                   summonzom();
                                   canspawn = false; // Prevent further spawns until the wait is complete
                                   StartCoroutine(WaitAfterSpawn());
                            }

                            movment();
                            shoot();

                     }

              }


       }

       void summonGhosts()
       {
              Instantiate(ghost, gameObject.transform);
       }

       void summonzom()
       {
              Instantiate(zombie, gameObject.transform);
       }


       private void shoot()
       {
              //Timer for shooting projectiles
              if (timeBtwShots <= 0) {
                     isAttacking = true;
                     //anim.SetTrigger("Attack");
                     Instantiate (projectile, transform.position, Quaternion.identity);
                     timeBtwShots = startTimeBtwShots;
              } else {
                     timeBtwShots -= Time.deltaTime;
                     isAttacking = false;
              }
       }



       private void movment(){
                                   // approach player
              if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
                     transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);

              } else if (Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance) {
                     // stop moving
                     transform.position = this.transform.position;
                     //anim.SetBool("Walk", false);
              }  else if (Vector2.Distance (transform.position, player.position) < retreatDistance) {
                            // retreat from player
                     transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
                     if (isAttacking == false) {
                            //anim.SetBool("Walk", true);
                     }
              }
       }

        private IEnumerator WaitAfterSpawn()
        {
            yield return new WaitForSeconds(spawn_cooldown); // Wait for 10 seconds
            canspawn = true; // Allow spawning again
        }

       void OnCollisionEnter2D(Collision2D collision){
              if (collision.gameObject.tag == "hairBall") {
                    enemyCurrHealth -= 1;
                    healthBar.UpdateHealth(enemyCurrHealth, enemyMaxHealth);
              }
                if (enemyCurrHealth <= 0)
                {
                    Destroy(gameObject);
                }
       }

      //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}



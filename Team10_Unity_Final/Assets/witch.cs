using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveShoot : MonoBehaviour {

      //public Animator anim;
       public float speed = 2f;
       public float stoppingDistance = 4f; // when enemy stops moving towards player
       public float retreatDistance = 3f; // when enemy moves away from approaching player
       private float timeBtwShots;
       public float startTimeBtwShots = 2;
       public GameObject projectile;

       private Rigidbody2D rb;
       private Transform player;
       private Vector2 PlayerVect;

       public int EnemyLives = 30;
       private Renderer rend;
       private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

       //
        private bool canspawn;
        public float spawn_cooldown;
        public witchSpawner spawner;

       void Start () {
              Physics2D.queriesStartInColliders = false;
              scaleX = gameObject.transform.localScale.x;

              rb = GetComponent<Rigidbody2D> ();
              player = GameObject.FindGameObjectWithTag("Player").transform;
              PlayerVect = player.transform.position;

              timeBtwShots = startTimeBtwShots;

              rend = GetComponentInChildren<Renderer> ();
              //anim = GetComponentInChildren<Animator> ();

              //if (GameObject.FindWithTag ("GameHandler") != null) {
              // gameHander = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
              //}

              canspawn = true;
              rb.constraints = RigidbodyConstraints2D.FreezeRotation;
       }

       void Update () {
              float DistToPlayer = Vector3.Distance(transform.position, player.position);
              if (DistToPlayer <= attackRange) {

                     // approach player
                     if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
                            transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
                            if (isAttacking == false) {
                                   //anim.SetBool("Walk", true);
                            }
                            //Vector2 lookDir = PlayerVect - rb.position;
                            //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
                            //rb.rotation = angle;
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

                    //spawn in zomvies if too close
                    if (DistToPlayer < 7f && canspawn)
                    {
                        spawner.spawn_enemy();
                        canspawn = false; // Prevent further spawns until the wait is complete
                        StartCoroutine(WaitAfterSpawn());
                    }

                     //Flip enemy to face player direction. Wrong direction? Swap the * -1.
                     if (player.position.x > gameObject.transform.position.x){
                            gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                    } else {
                             gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                     }

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
       }

        private IEnumerator WaitAfterSpawn()
        {
            yield return new WaitForSeconds(spawn_cooldown); // Wait for 10 seconds
            canspawn = true; // Allow spawning again
        }

       void OnCollisionEnter2D(Collision2D collision){
              if (collision.gameObject.tag == "hairBall") {
                    EnemyLives -= 1;
              }
                if (EnemyLives <= 0)
                {
                    Destroy(gameObject);
                }
       }

      //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}



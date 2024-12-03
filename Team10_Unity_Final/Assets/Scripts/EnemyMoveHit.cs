using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

       public Animator anim;
       public Rigidbody2D rb2D;
       public float speed = 4f;
       private Transform target;
       public int damage = 10;

       // Make the enemy lives more if the levels are higher --> do this later
       public int EnemyLives = 1;
       private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

       public float knockBackForce = 0f;

       private EnemyMeleeDamage meleeDamage;
       public SpriteToggle spriteToggle;
       
       void Start () {
              anim = GetComponentInChildren<Animator> ();
              rb2D = GetComponent<Rigidbody2D> ();
              scaleX = gameObject.transform.localScale.x;

              // Check if Player object is found
              if (GameObject.FindGameObjectWithTag("Player") != null) {
                     target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                     // Debug.LogError("check1");
              } else {
              Debug.LogError("Player object with tag 'Player' not found!");
              }

              // Check if GameHandler object is found
              if (GameObject.FindWithTag("GameHandler") != null) {
                     gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
                     // gameHandler.testPrint();

              } else {
              Debug.LogError("GameHandler object with tag 'GameHandler' not found!");
              }

              // Check if SpriteToggle component is found
              spriteToggle = FindObjectOfType<SpriteToggle>();
              if (spriteToggle == null) {
              Debug.LogWarning("SpriteToggle component not found! Ghost mode may not work as expected.");
              }

       }

       void Update () {
              if (spriteToggle != null && !spriteToggle.isGhostMode)
              {
                     float DistToPlayer = Vector3.Distance(transform.position, target.position);
                     if ((target != null) && (DistToPlayer <= attackRange)){
                         transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
                         //anim.SetBool("Walk", true);
                         //flip enemy to face player direction. Wrong direction? Swap the * -1.
                     if (target.position.x > gameObject.transform.position.x){
                         gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                     } else {
                         gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                     }
                     }
              }
              //else { anim.SetBool("Walk", false);}
       }

       public void OnCollisionEnter2D(Collision2D other){
              if (other.gameObject.tag == "hairBall") {
                     Debug.Log("hit by hairball");  // Specific log for hairball collision
                     EnemyLives -= 1;

                     if (EnemyLives == 0) {
                            Destroy(gameObject);
                     }
              }

              if (other.gameObject.tag == "Player") {
                     Debug.Log("hit by enemy");
                     isAttacking = true;
                     //anim.SetBool("Attack", true);
                     if (gameHandler != null) {
                            gameHandler.playerGetHit(damage);
                     } else {
                            Debug.LogError("GameHandler is not assigned!");
                     }


                     //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
                     //StartCoroutine(HitEnemy());

                     //Tell the player to STOP getting knocked back before getting knocked back:
                     //other.gameObject.GetComponent<Player_EndKnockBack>().EndKnockBack();
                     //Add force to the player, pushing them back without teleporting:
                     //Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
                     //Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
                     //pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
              }
       }


       public void OnCollisionExit2D(Collision2D other){
              if (other.gameObject.tag == "Player") {
                     isAttacking = false;
                     //anim.SetBool("Attack", false);
              }
       }

       //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}

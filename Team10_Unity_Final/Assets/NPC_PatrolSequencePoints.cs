using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC_PatrolSequencePoints : MonoBehaviour {
       // private Animator anim;
       public float speed = 2f;
       private float waitTime;
       public float startWaitTime = 1f;

        public float initAttackRange = 5f;
        public float postAttackRange = 10f;


    private bool attack;

        public int startHealth = 10; 
        private int currHealth = 10;

       public Transform[] moveSpots;
       public int startSpot = 0;
       public bool moveForward = true;

       // Turning
       private int nextSpot;
       private int previousSpot;
       public bool faceRight = false;

        public Transform target;  // Reference to the player's transform
        private GameHandler gameHandler;  // Reference to GameHandler
        private SpriteToggle spriteToggle;

        [SerializeField] floatingHealthBar healthBar;

       void Start(){
              waitTime = startWaitTime;
              nextSpot = startSpot;

              currHealth = startHealth;

            spriteToggle = FindObjectOfType<SpriteToggle>();


            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            

            healthBar = GetComponentInChildren<floatingHealthBar>();

            GameObject gameHandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
            if (gameHandlerObject != null) {
                gameHandler = gameHandlerObject.GetComponent<GameHandler>();
            }
       }



       void Update(){

            float distToPlayer = Vector3.Distance(transform.position, target.position);
            
            if (distToPlayer < initAttackRange) {
                attack = true;
            } else if (distToPlayer < postAttackRange && currHealth < startHealth){
                attack = true;
            } else {
                attack = false;
            }

            // Check if the player is within the attack range
            if (attack && !spriteToggle.isGhostMode) {
                    MoveTowardPlayer();
                
            } else {
                patrol();
            }
            KeepUpright();
              
       }

    void KeepUpright() {
    // Reset rotation to (0, 0, 0)
        transform.rotation = Quaternion.identity;
    }

    void MoveTowardPlayer() {
        if (target != null) {

            // Move the Urkai toward the player
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

     void patrol(){
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);

              if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) < 0.2f){
                     if (waitTime <= 0){
                            if (moveForward == true){ previousSpot = nextSpot; nextSpot += 1; }
                            else if (moveForward == false){ previousSpot = nextSpot; nextSpot -= 1; }
                            waitTime = startWaitTime;
                     } else {
                            waitTime -= Time.deltaTime;
                     }
              }

              //switch movement direction
              if (nextSpot == 0) {moveForward = true; }
              else if (nextSpot == (moveSpots.Length -1)) { moveForward = false; }

              //turning the enemy
              if (previousSpot < 0){ previousSpot = moveSpots.Length -1; }
              else if (previousSpot > moveSpots.Length -1){ previousSpot = 0; }

              if ((previousSpot == 0) && (faceRight)){ NPCTurn(); }
              else if ((previousSpot == (moveSpots.Length -1)) && (!faceRight)) { NPCTurn(); }
              // NOTE1: If faceRight does not change, try reversing !faceRight, above
              // NOTE2: If NPC faces the wrong direction as it moves, set the sprite Scale X = -1.

       }

       private void NPCTurn(){
              // NOTE: Switch player facing label (avoids constant turning)
              faceRight = !faceRight;

              // NOTE: Multiply player's x local scale by -1.
              Vector3 theScale = transform.localScale;
              theScale.x *= -1;
              transform.localScale = theScale;
       }


    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "hairBall") {
            Debug.Log("Hit by hairball");
            currHealth -= 1;
            healthBar.UpdateHealth(currHealth, startHealth);
            
            if(currHealth == 0){
                Destroy(gameObject);
            }
            
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        float damageCooldown = 1f;
        float nextDamageTime = 1f;
        if (other.gameObject.CompareTag("Player")) {
            if (Time.time >= nextDamageTime) {
                if (gameHandler != null) {
                    gameHandler.playerGetHit(30);  // Apply damage
                } else {
                    Debug.LogError("GameHandler is not assigned!");
                }
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

}
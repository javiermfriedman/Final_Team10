using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC_PatrolSequencePoints : MonoBehaviour {
       // private Animator anim;
       public float speed = 10f;
       private float waitTime;
       public float startWaitTime = 2f;
    private float attackRange = 5f;
    
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

       void Start(){
              waitTime = startWaitTime;
              nextSpot = startSpot;

            spriteToggle = FindObjectOfType<SpriteToggle>();
            if (spriteToggle == null) {
                Debug.LogWarning("SpriteToggle component not found! Ghost mode may not work as expected.");
            }

            if (GameObject.FindGameObjectWithTag("Player") != null) {
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }


            GameObject gameHandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
            if (gameHandlerObject != null) {
                gameHandler = gameHandlerObject.GetComponent<GameHandler>();
            }
       }



       void Update(){
            float distToPlayer = Vector3.Distance(transform.position, target.position);

            // Check if the player is within the attack range
            if (distToPlayer < attackRange) {
                MoveTowardPlayer();
            } else {
                patrol();
            }
              
       }

    void MoveTowardPlayer() {
        if (target != null) {
            float speed = 3f;

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

}
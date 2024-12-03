using System.Collections;
using UnityEngine;

public class NPC_PatrolRandomPoints : MonoBehaviour
{
// private Animator anim;
       public float speed = 10f;
       private float waitTime;
       public float startWaitTime = 2f;

       public Transform[] moveSpots;
       public int startSpot = 0;
       public bool moveForward = true;

       // Turning
       private int nextSpot;
       private int previousSpot;
       public bool faceRight = false;

       void Start(){
              waitTime = startWaitTime;
              nextSpot = startSpot;
              //anim = gameObject.GetComponentInChildren<Animator>();
       }

       void Update(){
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

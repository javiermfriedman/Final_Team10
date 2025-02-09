using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class playerMove : MonoBehaviour {

      public Animator anim1;
      public Animator anim2;
      //public AudioSource WalkSFX;
      private Rigidbody2D rb2D;
      private bool FaceRight = false; // determine which way player is facing.
      public static float runSpeed = 4f;
      public float startSpeed = 10f;
      public bool isAlive = true;

      private GameHandler gameHandler;

      void Start(){
           //anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();

                   if (gameHandler == null)
                  {
                        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
                  }
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
           if (isAlive == true){
                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

                  if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                        anim1.SetBool ("Walk", true);
                        anim2.SetBool ("Walk", true);
                  //     if (!WalkSFX.isPlaying){
                  //           WalkSFX.Play();
                  //     }
                  } else {
                       anim1.SetBool ("Walk", false);
                       anim2.SetBool ("Walk", false);
                  //     WalkSFX.Stop();
                 }

                  // Turning. Reverse if input is moving the Player right and Player faces left.
                 if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                        playerTurn();
                  }
            }
      }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "witch_proj")
        {
            gameHandler.playerGetHit(10);
        }

    }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }
}
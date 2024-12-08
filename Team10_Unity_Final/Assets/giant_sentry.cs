// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class NPC_PatrolSequencePoints : MonoBehaviour {
//        // private Animator anim;
//        public float speed = 2f;

//         public int startHealth = 10; 
//         private int currHealth = 10;

//        // Turning

//         public Transform target;  // Reference to the player's transform
//         private GameHandler gameHandler;  // Reference to GameHandler
//         private SpriteToggle spriteToggle;

//         [SerializeField] floatingHealthBar healthBar;

//        void Start(){

//             currHealth = startHealth;

//             spriteToggle = FindObjectOfType<SpriteToggle>();


//             target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            

//             healthBar = GetComponentInChildren<floatingHealthBar>();

//             GameObject gameHandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
//             if (gameHandlerObject != null) {
//                 gameHandler = gameHandlerObject.GetComponent<GameHandler>();
//             }
//        }



//        void Update(){

//             float distToPlayer = Vector3.Distance(transform.position, target.position);
            
//             if (distToPlayer < initAttackRange) {
//                 attack = true;
//             } else if (distToPlayer < postAttackRange && currHealth < startHealth){
//                 attack = true;
//             } else {
//                 attack = false;
//             }

//             // Check if the player is within the attack range
//             if (attack) {
//                 MoveTowardPlayer();
//             } else {
//                 patrol();
//             }
//             KeepUpright();
              
//        }

//     void KeepUpright() {
//     // Reset rotation to (0, 0, 0)
//         transform.rotation = Quaternion.identity;
//     }

//     void MoveTowardPlayer() {
//         if (target != null) {

//             // Move the Urkai toward the player
//             transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
//         }
//     }



//     void OnCollisionEnter2D(Collision2D other) {
//         if (other.gameObject.tag == "hairBall") {
//             Debug.Log("Hit by hairball");
//             currHealth -= 1;
//             healthBar.UpdateHealth(currHealth, startHealth);
            
//             if(currHealth == 0){
//                 Destroy(gameObject);
//             }
            
//         }
//     }

//     void OnCollisionStay2D(Collision2D other) {
//         float damageCooldown = 1f;
//         float nextDamageTime = 1f;
//         if (other.gameObject.CompareTag("Player")) {
//             if (Time.time >= nextDamageTime) {
//                 if (gameHandler != null) {
//                     gameHandler.playerGetHit(30);  // Apply damage
//                 } else {
//                     Debug.LogError("GameHandler is not assigned!");
//                 }
//                 nextDamageTime = Time.time + damageCooldown;
//             }
//         }
//     }

// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giantSentry : MonoBehaviour
{
    // Start is called before the first frame update

       private float speed = 2f;
       private float attackRange = 20f;

        private int startHealth = 10; 
        private int currHealth = 10;

       // Turning

       private bool attack;

        private Transform target;  // Reference to the player's transform
        private GameHandler gameHandler;  // Reference to GameHandler
        private SpriteToggle spriteToggle;

        [SerializeField] floatingHealthBar healthBar;
    void Start()
    {
        attack = false;
        currHealth = startHealth;

        spriteToggle = FindObjectOfType<SpriteToggle>();


        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        

        healthBar = GetComponentInChildren<floatingHealthBar>();

        GameObject gameHandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        if (gameHandlerObject != null) {
            gameHandler = gameHandlerObject.GetComponent<GameHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
            float distToPlayer = Vector3.Distance(transform.position, target.position);
        

            // Check if the player is within the attack range
            if (distToPlayer < attackRange && !spriteToggle.isGhostMode) {
                MoveTowardPlayer();
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

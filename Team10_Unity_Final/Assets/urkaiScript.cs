using UnityEngine;

public class Urkai : MonoBehaviour {
    public float walkSpeed = 2f;  // Walking speed
    public float sprintSpeed = 5f;  // Sprinting speed
    private float attackRange = 5f;  // The range within which Urkai sprints at the player

    public Transform target;  // Reference to the player's transform
    private GameHandler gameHandler;  // Reference to GameHandler
    private SpriteToggle spriteToggle;  // Reference to the SpriteToggle component
    private float scaleX;  // Store the default scale of the Urkai

    private float timer;
    private float timeToChangeDirection = 10f;

    void Start() {
        // Find the GameHandler in the scene by tag
        GameObject gameHandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        if (gameHandlerObject != null) {
            gameHandler = gameHandlerObject.GetComponent<GameHandler>();
        } else {
            Debug.LogError("GameHandler not found!");
        }

        // Find the SpriteToggle component
        spriteToggle = FindObjectOfType<SpriteToggle>();
        if (spriteToggle == null) {
            Debug.LogWarning("SpriteToggle component not found! Ghost mode may not work as expected.");
        }

        // Find the player object
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        } else {
            Debug.LogError("Player object with tag 'Player' not found!");
        }

        // Store the default scale of the Urkai
        scaleX = gameObject.transform.localScale.x;
    }

    void Update() {
        // Only move the Urkai when not in Ghost mode

        float distToPlayer = Vector3.Distance(transform.position, target.position);

        // Debug the distance for better visibility
        Debug.Log("Distance to player is: " + distToPlayer);
        Debug.Log("player range is  " + attackRange);

        // Check if the player is within the attack range
        if (distToPlayer < attackRange) {
            Debug.LogError("I see you! Player is within attack range.");
            MoveTowardPlayer();
        } else {
            Debug.LogError("No, player is too far.");
            naturalState();

        }
    }



void naturalState() {
        float speed = 0.5f;  // Speed of movement
        float width = 3f;  // Width of the rectangle (horizontal movement)
        float height = 2f; // Height of the rectangle (vertical movement)
        
        // Calculate the movement based on sine wave for smooth oscillation
        float x = Mathf.Sin(Time.time * speed) * width;
        float y = Mathf.Cos(Time.time * speed) * height;

        // Update the position of the zombie
        transform.position = new Vector3(x, y, transform.position.z);
}


    void MoveTowardPlayer() {
        if (target != null) {
            // Calculate the distance to the player

            float distToPlayer = Vector3.Distance(transform.position, target.position);

            // Choose the appropriate speed (sprint if in range, otherwise walk)
            // float speed = distToPlayer <= attackRange ? sprintSpeed : walkSpeed;
            float speed = 5f;

            // Move the Urkai toward the player
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Flip the Urkai to face the player (left or right)
        }
    }

    // void FlipUrkaiFacing() {
    //     // Flip the Urkai's direction based on the player's position
    //     if (target.position.x > transform.position.x) {
    //         transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
    //     } else {
    //         transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
    //     }
    // }

    void OnCollisionStay2D(Collision2D other) {
        float damageCooldown = 1f;
        float nextDamageTime = 1f;
        // Check if the object we collided with is the Player
        if (other.gameObject.CompareTag("Player")) {
            // Only apply damage every 1 second (damageCooldown)
            if (Time.time >= nextDamageTime) {
                if (gameHandler != null) {
                    gameHandler.playerGetHit(10);  // Apply damage (adjust as needed)
                    Debug.Log("Player is being hit by Urkai");
                } else {
                    Debug.LogError("GameHandler is not assigned!");
                }

                // Set the next allowed damage time (1 second cooldown)
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}

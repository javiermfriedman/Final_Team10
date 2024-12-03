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

    public Transform homeBase;
    private Vector3 initialPosition;  // Store the starting position of Urkai

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

        // Store the initial position of the Urkai
        initialPosition = homeBase.position;
    }

    void Update() {
        float distToPlayer = Vector3.Distance(transform.position, target.position);

        // Check if the player is within the attack range
        if (distToPlayer < attackRange) {
            MoveTowardPlayer();
        } else {
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

        // Update the position relative to the initial position
        transform.position = new Vector3(initialPosition.x + x, initialPosition.y + y, transform.position.z);
    }

    void MoveTowardPlayer() {
        if (target != null) {
            float speed = 5f;

            // Move the Urkai toward the player
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "hairBall") {
            Debug.Log("Hit by hairball");
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        float damageCooldown = 1f;
        float nextDamageTime = 1f;
        if (other.gameObject.CompareTag("Player")) {
            if (Time.time >= nextDamageTime) {
                if (gameHandler != null) {
                    gameHandler.playerGetHit(10);  // Apply damage
                } else {
                    Debug.LogError("GameHandler is not assigned!");
                }
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}

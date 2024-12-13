using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC_PatrolSequencePoints : MonoBehaviour {
    public float speed = 2f;
    private float waitTime;
    public float startWaitTime = 1f;

    public float initAttackRange = 5f;
    public float postAttackRange = 10f;

    public int startHealth = 10; 
    private int currHealth = 10;

    public Transform[] moveSpots;
    public int startSpot = 0;
    public bool moveForward = true;

    public bool faceRight = false;

    public Transform target;  // Reference to the player's transform
    private GameHandler gameHandler;  // Reference to GameHandler
    private SpriteToggle spriteToggle;

    [SerializeField] private floatingHealthBar healthBar;

    private Vector3 lastKnownPlayerPosition; // Logs player's last transform before ghost mode
    private bool isChasingGhost;

    private int nextSpot; // Current target spot
    private int previousSpot; // Previous spot for direction handling

    void Start() {
        waitTime = startWaitTime;
        currHealth = startHealth;

        spriteToggle = FindObjectOfType<SpriteToggle>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthBar = GetComponentInChildren<floatingHealthBar>();

        GameObject gameHandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        if (gameHandlerObject != null) {
            gameHandler = gameHandlerObject.GetComponent<GameHandler>();
        }

        // Initialize patrol variables
        nextSpot = startSpot;
        previousSpot = startSpot;
    }

    void Update() {
        float distToPlayer = Vector3.Distance(transform.position, target.position);

        bool inAttackRange = distToPlayer < initAttackRange || 
                             (distToPlayer < postAttackRange && currHealth < startHealth);

        if (inAttackRange) {
            if (spriteToggle.isGhostMode) {
                if (!isChasingGhost) {
                    lastKnownPlayerPosition = target.position; // Log position once
                    isChasingGhost = true;
                }
                MoveTowardLastKnownPosition();
            } else {
                isChasingGhost = false;
                MoveTowardPlayer();
            }
        } else {
            patrol();
        }
        
        KeepUpright();
    }

    void KeepUpright() {
        transform.rotation = Quaternion.identity;
    }

    void MoveTowardPlayer() {
        if (target != null) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void MoveTowardLastKnownPosition() {
        transform.position = Vector2.MoveTowards(transform.position, lastKnownPlayerPosition, speed * Time.deltaTime);

        // Stop chasing ghost if the NPC reaches the last known position
        if (Vector3.Distance(transform.position, lastKnownPlayerPosition) < 0.1f) {
            isChasingGhost = false;
        }
    }

    void patrol() {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) < 0.2f) {
            if (waitTime <= 0) {
                previousSpot = nextSpot;
                nextSpot = moveForward ? nextSpot + 1 : nextSpot - 1;
                waitTime = startWaitTime;
            } else {
                waitTime -= Time.deltaTime;
            }
        }

        if (nextSpot == 0) {
            moveForward = true;
        } else if (nextSpot == moveSpots.Length - 1) {
            moveForward = false;
        }

        if ((previousSpot == 0 && faceRight) || 
            (previousSpot == moveSpots.Length - 1 && !faceRight)) {
            NPCTurn();
        }
    }

    private void NPCTurn() {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "hairBall") {
            Debug.Log("Hit by hairball");
            currHealth -= 1;
            healthBar.UpdateHealth(currHealth, startHealth);

            if (currHealth == 0) {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        float damageCooldown = 2f;
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

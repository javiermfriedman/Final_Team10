using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giantSentry : MonoBehaviour
{
    private float speed = 2f;
    private float attackRange = 20f;

    public int startHealth = 15; 
    public int currHealth = 14;

    private bool attack;
    private Transform target;
    private GameHandler gameHandler;
    private SpriteToggle spriteToggle;

    [SerializeField] private floatingHealthBar healthBar;

    private Vector3 lastKnownPlayerPosition; // Stores the last transform of the player before ghost mode
    private bool isChasingGhost;

    private bool faceRight = true; // Track the current direction of the NPC

    void Start()
    {
        attack = false;

        spriteToggle = FindObjectOfType<SpriteToggle>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthBar = GetComponentInChildren<floatingHealthBar>();

        GameObject gameHandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        if (gameHandlerObject != null)
        {
            gameHandler = gameHandlerObject.GetComponent<GameHandler>();
        }
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, target.position);

        if (currHealth < startHealth && distToPlayer < attackRange)
        {
            if (spriteToggle.isGhostMode)
            {
                if (!isChasingGhost) // Log player's last position only once
                {
                    lastKnownPlayerPosition = target.position;
                    isChasingGhost = true;
                }
                MoveTowardLastKnownPosition();
            }
            else
            {
                isChasingGhost = false;
                MoveTowardPlayer();
            }
        }

        KeepUpright();
    }

    void KeepUpright()
    {
        transform.rotation = Quaternion.identity;
    }

    void MoveTowardPlayer()
    {
        if (target != null)
        {
            HandleDirection(target.position.x); // Check direction and turn
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void MoveTowardLastKnownPosition()
    {
        HandleDirection(lastKnownPlayerPosition.x); // Check direction and turn
        transform.position = Vector2.MoveTowards(transform.position, lastKnownPlayerPosition, speed * Time.deltaTime);

        // Stop chasing if the sentry reaches the last known position
        if (Vector3.Distance(transform.position, lastKnownPlayerPosition) < 0.1f)
        {
            isChasingGhost = false;
        }
    }

    void HandleDirection(float targetXPosition)
    {
        bool shouldFaceRight = targetXPosition > transform.position.x;
        if (shouldFaceRight != faceRight)
        {
            NPCTurn();
        }
    }

    void NPCTurn()
    {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "hairBall")
        {
            Debug.Log("Hit by hairball");
            currHealth -= 1;
            healthBar.UpdateHealth(currHealth, startHealth);

            if (currHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        float damageCooldown = 2f;
        float nextDamageTime = 1f;
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime)
            {
                if (gameHandler != null)
                {
                    gameHandler.playerGetHit(10);  // Apply damage
                }
                else
                {
                    Debug.LogError("GameHandler is not assigned!");
                }
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb2D;
    public float speed = 4f;
    private Transform target;
    public int damage = 10;
    private float waitTime;

    public int EnemyLives = 1;
    private GameHandler gameHandler;

    public float attackRange = 10;
    public bool isAttacking = false;
    private float scaleX;

    public float knockBackForce = 0f;

    private EnemyMeleeDamage meleeDamage;
    public SpriteToggle spriteToggle;

    private Coroutine damageCoroutine; // Reference to the damage coroutine

    [Header("Audio Settings")]
    public AudioClip zombieGrowl; // Sound when the zombie sees the player
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        scaleX = gameObject.transform.localScale.x;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }

        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
        else
        {
            Debug.LogError("GameHandler object with tag 'GameHandler' not found!");
        }

        spriteToggle = FindObjectOfType<SpriteToggle>();
        if (spriteToggle == null)
        {
            Debug.LogWarning("SpriteToggle component not found! Ghost mode may not work as expected.");
        }
    }

    void Update()
    {
        if (spriteToggle != null && !spriteToggle.isGhostMode)
        {
            float DistToPlayer = Vector3.Distance(transform.position, target.position);
            if ((target != null) && (DistToPlayer <= attackRange))
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                if (target.position.x > gameObject.transform.position.x)
                {
                    gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                }
                else
                {
                    gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "hairBall")
        {
            EnemyLives -= 1;

            if (EnemyLives == 0)
            {
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            isAttacking = true;

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(other.gameObject));
            }

            //Play attack sound
            if (zombieGrowl != null)
            {
                audioSource.PlayOneShot(zombieGrowl);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAttacking = false;

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageOverTime(GameObject player)
    {
        while (isAttacking)
        {
            if (gameHandler != null)
            {
                gameHandler.playerGetHit(damage);
            }
            else
            {
                Debug.LogError("GameHandler is not assigned!");
            }

            yield return new WaitForSeconds(1f); // Apply damage every second
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}


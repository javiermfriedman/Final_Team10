using System.Collections;
using UnityEngine;

public class NPC_PatrolRandomSpace : MonoBehaviour
{
    public float speed = 10f;
    private float waitTime;
    public float startWaitTime = 2f;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Variables for distance from player when in fire range
    public float minDistanceFromPlayer = 10f;
    public float maxDistanceFromPlayer = 14f;

    // Shooting variables
    public GameObject projectile;
    public GameObject firePoint;
    public float attackRange = 10f;
    public float startTimeBtwShots = 2f;
    private float timeBtwShots;
    private Transform player;

    private bool playerInAttackRange = false;
    private bool isReversing = false;

    public LayerMask obstacleLayer; // Layer for obstacles

    void Start()
    {
        waitTime = startWaitTime;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomMoveSpot();
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        if (isReversing) return;

        // Patrol logic
        Vector2 direction = (moveSpot.position - transform.position).normalized;
        RotateTowards(direction);

        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                if (playerInAttackRange)
                {
                    SetMoveSpotNearPlayer();
                }
                else
                {
                    SetRandomMoveSpot();
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        // Shooting logic
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (CanSeePlayer())
            {
                if (distanceToPlayer <= attackRange){
                    playerInAttackRange = true;
                }
                    

                Vector2 playerDirection = (player.position - transform.position).normalized;
                RotateTowards(playerDirection);

                if (timeBtwShots <= 0)
                {
                    Shoot();
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// Checks if there are no obstacles between the NPC and the player.
    /// </summary>
    /// <returns>True if the NPC can see the player; otherwise, false.</returns>
    bool CanSeePlayer()
    {
        Vector2 firePointPosition = firePoint != null ? firePoint.transform.position : transform.position;
        RaycastHit2D hit = Physics2D.Linecast(firePointPosition, player.position, obstacleLayer);

        // Return true if no collider is hit (line of sight is clear)
        return hit.collider == null;
    }


    void SetRandomMoveSpot()
    {
        Vector2 randomSpot;
        do
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            randomSpot = new Vector2(randomX, randomY);
        }
        while (Physics2D.Linecast(transform.position, randomSpot, obstacleLayer)); // Ensure no obstacle in path

        moveSpot.position = randomSpot;
    }

    void SetMoveSpotNearPlayer()
    {
        if (player == null) return;

        Vector2 newSpot;
        do
        {
            // Random angle for direction
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            // Random distance between min and max range
            float distance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

            // Calculate new position relative to player
            float offsetX = Mathf.Cos(angle) * distance;
            float offsetY = Mathf.Sin(angle) * distance;
            newSpot = new Vector2(player.position.x + offsetX, player.position.y + offsetY);
        }
        while (Physics2D.Linecast(transform.position, newSpot, obstacleLayer)); // Ensure no obstacle in path

        moveSpot.position = newSpot;
    }

    void RotateTowards(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * speed);
    }

    void Shoot()
    {
        if (firePoint != null && projectile != null)
        {
            Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && !isReversing)
        {
            StartCoroutine(ReverseAndFindNewSpot(collision));
        }
    }

    IEnumerator ReverseAndFindNewSpot(Collision2D collision)
    {
        isReversing = true;

        Vector2 reverseDirection = collision.GetContact(0).normal;
        float reverseTime = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < reverseTime)
        {
            transform.position += (Vector3)(reverseDirection * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetRandomMoveSpot();
        yield return new WaitForSeconds(0.5f);
        isReversing = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

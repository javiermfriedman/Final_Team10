using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float accelerationTime = 2f; // Time interval to change direction
    public float maxSpeed = 10f;         // Maximum speed
    public float movementRadius = 5f;  // Radius within which the GameObject can move
    private Vector2 movement;          // Stores the movement direction
    private float timeLeft;            // Countdown timer
    private Rigidbody2D rb;            // Reference to the Rigidbody2D component
    private Vector2 startingPosition;  // Starting position of the GameObject

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from this GameObject.");
        }
        startingPosition = transform.position; // Store the starting position
        timeLeft = accelerationTime;          // Initialize the timer

        // Initialize movement with a random direction
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            // Generate a random direction
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            timeLeft = accelerationTime; // Reset the timer
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Move the GameObject
            rb.velocity = movement * maxSpeed;

            // Keep it within the movement radius
            if (Vector2.Distance(startingPosition, rb.position) > movementRadius)
            {
                // Reflect the direction back towards the center
                movement = (startingPosition - rb.position).normalized;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the movement vector based on the collision normal
        Vector2 normal = collision.contacts[0].normal;
        movement = Vector2.Reflect(movement, normal).normalized;

        // Apply the reflected direction
        rb.velocity = movement * maxSpeed;
    }
}

using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float accelerationTime = 2f; // Time interval to change direction
    public float maxSpeed = 2f;         // Maximum speed
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
            // Calculate the next position
            Vector2 nextPosition = rb.position + movement * maxSpeed * Time.fixedDeltaTime;

            // Check if the next position is within the radius
            if (Vector2.Distance(startingPosition, nextPosition) <= movementRadius)
            {
                rb.AddForce(movement * maxSpeed); // Apply force if within bounds
            }
            else
            {
                // Reflect the direction back towards the starting position
                movement = (startingPosition - rb.position).normalized;
                rb.AddForce(movement * maxSpeed); // Adjust force to keep it inside the radius
            }
        }
    }
}

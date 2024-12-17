using UnityEngine;

public class WanderingNPC : MonoBehaviour
{
    public float moveSpeed = 2f;        // Speed of the NPC
    public float areaRadius = 5f;      // Radius within which the NPC can wander
    public float waitTime = 2f;        // Time the NPC waits before moving to a new location

    private Vector3 startPosition;     // The starting position of the NPC
    private Vector3 targetPosition;    // The NPC's next destination
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
        SetRandomDestination();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            Invoke(nameof(SetRandomDestination), waitTime);
        }
    }

    void SetRandomDestination()
    {
        Vector2 randomPoint = Random.insideUnitCircle * areaRadius;
        targetPosition = startPosition + new Vector3(randomPoint.x, 0, randomPoint.y);
        isMoving = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPosition == Vector3.zero ? transform.position : startPosition, areaRadius);
    }
}

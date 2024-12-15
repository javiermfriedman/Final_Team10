using UnityEngine;

public class Compass : MonoBehaviour
{
    [Header("References")]
    public Transform player; // The player Transform
    public Transform totem;  // The target Transform (e.g., Totem)

    [Header("Compass Settings")]
    public RectTransform compassNeedle; // The UI element (RectTransform) of the compass needle

    void Update()
    {
        if (player == null || totem == null || compassNeedle == null)
        {
            Debug.LogWarning("Missing references in the Compass script.");
            return;
        }

        // Calculate direction from player to the totem in 2D space
        Vector2 playerPosition = new Vector2(player.position.x, player.position.y);
        Vector2 totemPosition = new Vector2(totem.position.x, totem.position.y);

        // Direction vector
        Vector2 directionToTotem = totemPosition - playerPosition;

        // Ensure the direction vector is valid
        if (directionToTotem.sqrMagnitude > 0.01f)
        {
            // Calculate the angle in degrees using Atan2 (Y over X)
            float angle = Mathf.Atan2(directionToTotem.y, directionToTotem.x) * Mathf.Rad2Deg;

            // Adjust the angle for the compass orientation
            compassNeedle.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}

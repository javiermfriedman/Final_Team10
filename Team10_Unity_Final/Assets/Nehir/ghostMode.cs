using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMode : MonoBehaviour
{
    private Collider2D playerCollider;
    private SpriteToggle spriteToggle;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>(); // Use Collider2D for 2D games
        spriteToggle = GetComponent<SpriteToggle>();

        // Check if the 2D collider exists
        if (playerCollider == null)
        {
            Debug.LogError("No Collider2D found on the Player GameObject. Please add one in the Inspector.");
        }
    }

    void Update()
    {
        // Proceed only if playerCollider and spriteToggle are available
        if (playerCollider != null && spriteToggle != null)
        {
            playerCollider.enabled = !spriteToggle.isGhostMode;
        }
    }
}

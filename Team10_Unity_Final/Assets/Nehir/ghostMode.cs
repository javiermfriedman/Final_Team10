using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMode : MonoBehaviour
{
    private Collider2D playerCollider;
    private SpriteToggle spriteToggle;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>(); 
        spriteToggle = GetComponent<SpriteToggle>();
    }

    void Update()
    {
        // Check that sprite toggle is not null to avoid any issues
        if (spriteToggle != null)
        {
            // Have the collider enabled if not in ghost mode, otherwise not enable it
            playerCollider.enabled = !spriteToggle.isGhostMode;
        }
    }
}

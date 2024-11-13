using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMode : MonoBehaviour
{
    private Collider2D playerCollider;
    private SpriteToggle spriteToggle;

    public string TagToIgnore = "Ignored";

    void Start()
    {
        playerCollider = GetComponent<Collider2D>(); 
        spriteToggle = GetComponent<SpriteToggle>();
    }

    void Update()
    {
        // // Check that sprite toggle is not null to avoid any issues
        // if (spriteToggle != null)
        // {
        //     // Have the collider enabled if not in ghost mode, otherwise not enable it
        //     playerCollider.enabled = !spriteToggle.isGhostMode;
        // }

        if (collision.gameObject.tag == TagToIgnore && spriteToggle.isGhostMode == true){
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMode : MonoBehaviour
{
    // private Collider2D playerCollider;
    private SpriteToggle spriteToggle;

    public string TagToIgnore = "Ignored";

    void Start()
    {
        spriteToggle = GetComponent<SpriteToggle>();
    }


    void OnCollisionEnter2D (Collision2D collision){
            if (collision.gameObject.tag == "Ignored"){
                if (spriteToggle != null){
                    Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                    
            }
    }
}

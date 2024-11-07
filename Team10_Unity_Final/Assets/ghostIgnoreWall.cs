using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class IgnoreWallCat : MonoBehaviour {

       public GameObject enemy;
       public string TagToIgnore = "Ignored";

       void OnCollisionEnter2D (Collision2D collision){
              if (collision.gameObject.tag == TagToIgnore && Input.GetKeyDown(KeyCode.Space)){
                     Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
              }
       }
}
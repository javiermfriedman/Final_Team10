using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieBall : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D other){

         

        if (other.gameObject.tag != "Player") {
              Destroy(gameObject); 
        }
    }

}

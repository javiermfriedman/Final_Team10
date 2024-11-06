using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player_EndKnockBack : MonoBehaviour {

       private Rigidbody2D playerRB;

       public void EndKnockBack(){
              playerRB = gameObject.GetComponent<Rigidbody2D>();
              StartCoroutine(EndKnockBack(playerRB));
       }

       IEnumerator EndKnockBack(Rigidbody2D myRB){
              yield return new WaitForSeconds(0.2f);
              myRB.velocity= new Vector3(0,0,0);
       }
}
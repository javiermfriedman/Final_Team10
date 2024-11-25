using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragCollect : MonoBehaviour
{
    public int fragValue = 1; // Adjust this for different token values

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call GameHandler to increment tokens
            GameObject gameHandler = GameObject.FindWithTag("GameHandler");
            if (gameHandler != null)
            {
                Debug.Log("The frag count in FragCollect: " + fragValue);
                gameHandler.GetComponent<GameHandler>().playerGetTokens(fragValue);
            } 

            // Destroy the token
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // This function is called when another 2D collider enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hitting Portal");
            // Load the specified scene
            SceneManager.LoadScene("Nate_test");
        }
    }
}
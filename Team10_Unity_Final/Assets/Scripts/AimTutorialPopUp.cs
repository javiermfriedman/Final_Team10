using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Use this if you're using the standard UI Text
using UnityEngine.SceneManagement; // Required to get the active scene

public class AimTutorialPopUp : MonoBehaviour
{
    public Text popupText; // Reference to the Text component
    private bool isPlayerInRange = false; // Flag to check if the player is in range

    void Start()
    {
        if (popupText != null)
        {
            popupText.text = ""; // Make sure the text is empty at the start
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (popupText != null)
            {
                popupText.text = "Aim with mouse and left click to shoot enemies!\n(Press R to close)"; // Set the message
            }
        }
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     // Clear the text when the player leaves the trigger area
    //     if (other.CompareTag("Player"))
    //     {
    //         isPlayerInRange = false;
    //         if (popupText != null)
    //         {
    //             popupText.text = ""; // Clear the text
    //         }
    //     }
    // }

    void Update()
    {
        // Check for 'R' key press if the player is in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            if (popupText != null)
            {
                popupText.text = ""; // Clear the text
            }
        }
    }
}
